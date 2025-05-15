using UnityEngine;
using Snog.EnemyFSM.Core;
using Snog.EnemyFSM.Configs.StateConfigs;
using Snog.EnemyFSM.Enemy.Services;

namespace Snog.EnemyFSM.Enemy.States
{
    /// <summary>
    /// Enemy wanders randomly until time runs out
    /// Every position is picked within a range around the enemy
    /// But there's a hance os bias, which causes the enemy to choose 
    /// a position on a range around the player instead
    /// </summary>
    public class WanderState : IState
    {
        #region Fields
        private readonly StateContext _ctx;
        private readonly WanderConfig _config;

        private floar timer = 0;
        private Vector3 _currentTarget;
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

            timer += Time.deltaTime;

            // if time's up...
            if(timer > _config.wanderDuration)
            {
                _ctx.Owner
                    .GetComponent<EnemyStateMachine>()
                    .StartStalk(); // stalk
            }

            // If we've reached it...
            if (Vector3.Distance(_ctx.Owner.position, _currentTarget) < 0.5f)
            {
                PickNewTarget(); // pick another target
            }

            // If player seen...
            if (_ctx.Vision.CanSeePlayer(_ctx.Player))
            {
                _ctx.Owner
                    .GetComponent<EnemyStateMachine>()
                    .StartChase(); // chase
            }
        }

        public void Exit() { }

        #endregion

        #region Helpers 
        private void PickNewTarget()
        {
            _currentTarget = WanderUtils.GetNextWanderTarget(
                                _ctx.Owner.position,
                                _config.wanderCircleRadius,
                                _config.biasChancePercentage,
                                _config.biasAccuracy,
                                _ctx.Player.position
                            );
        }
        #endregion
    }
}
