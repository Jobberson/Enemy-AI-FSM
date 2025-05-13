using UnityEngine;
using Snog.EnemyFSM.Core;
using Snog.EnemyFSM.Configs.StateConfigs;
using Snog.EnemyFSM.Enemy.Services;

namespace Snog.EnemyFSM.Enemy.States
{
    /// <summary>
    /// Enemy wanders randomly within a circle until it spots the player.
    /// </summary>
    public class WanderState : IState
    {
        #region Fields
        private readonly StateContext _ctx;
        private readonly WanderConfig _config;

        private Vector3 _currentTarget;
        private float   _timeUntilNextTarget;
        #endregion

        #region Constructor
        public WanderState(StateContext context, WanderConfig config)
        {
            _ctx    = context;
            _config = config;
        }
        #endregion

        #region IState Implementation

        public void Enter()
        {
            // Immediately pick first target
            PickNewTarget();
        }

        public void Tick()
        {
            // Move toward current target
            _ctx.Movement.MoveTo(_currentTarget, _ctx.EnemyConfig.wanderSpeed);

            // If we've reached it (or time elapsed), pick another
            _timeUntilNextTarget -= Time.deltaTime;
            if (_timeUntilNextTarget <= 0f ||
                Vector3.Distance(_ctx.Owner.position, _currentTarget) < 0.5f)
            {
                PickNewTarget();
            }

            // Transition if player seen
            if (_ctx.Vision.CanSeePlayer(_ctx.Player))
            {
                _ctx.Owner
                    .GetComponent<EnemyStateMachine>()
                    .EngageChase();
            }
        }

        public void Exit() { }

        #endregion

        #region Helpers (delegated to WanderUtils)
        private void PickNewTarget()
        {
            _currentTarget      = WanderUtils.GetNextWanderTarget(
                                      _ctx.Owner.position,
                                      _config.wanderCircleRadius,
                                      _config.biasChancePercentage,
                                      _config.biasAccuracy,
                                      _ctx.Player.position);
            _timeUntilNextTarget = _config.wanderDuration;
        }
        #endregion
    }
}
