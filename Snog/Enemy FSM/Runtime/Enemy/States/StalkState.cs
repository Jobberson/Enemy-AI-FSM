using UnityEngine;
using Snog.EnemyFSM.Core;
using Snog.EnemyFSM.Configs.StateConfigs;
using Snog.EnemyFSM.Enemy.Services;

namespace Snog.EnemyFSM.Enemy.States
{
    /// <summary>
    /// Enemy Stalks randomly within a circle until it spots the player.
    /// </summary>
    public class StalkState : IState
    {
        #region Fields
        private readonly StateContext _ctx;
        private readonly StalkConfig _config;
        private float timer;
        #endregion

        #region Constructor
        public StalkState(StateContext context, StalkConfig config)
        {
            _ctx    = context;
            _config = config;
        }
        #endregion

        #region IState Implementation

        public void Enter()
        {
            _ctx.AStarController.aIPath.endReachedDistance = _config.stalkingDistance;
        }

        public void Tick()
        {   
            // Move toward current target
            _ctx.Movement.MoveTo(_ctx.Player, _ctx.EnemyConfig.investigateSpeed);

            // countdown for the duration
            timer += Time.deltaTime;
            if(timer >= _config.stalkDuration)
            {
                timer = 0f;

                _ctx.Owner
                    .GetComponent<EnemyStateMachine>()
                    .ReturnToWander(); // if time is up for stalking
            }

            // Transition if player seen
            if (_ctx.Vision.CanSeePlayer(_ctx.Player))
            {
                _ctx.Owner
                    .GetComponent<EnemyStateMachine>()
                    .EngageChase();
            }
        }

        public void Exit() 
        {
            _ctx.AStarController.aIPath.endReachedDistance = 1.2f;
        }
        #endregion
    }
}
