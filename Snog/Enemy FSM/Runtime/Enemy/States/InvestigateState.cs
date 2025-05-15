using UnityEngine;
using Snog.EnemyFSM.Core;
using Snog.EnemyFSM.Configs.StateConfigs;
using Snog.EnemyFSM.Enemy.Services;

namespace Snog.EnemyFSM.Enemy.States
{
    /// <summary>
    /// Enemy Investigates randomly within a circle until it spots the player.
    /// </summary>
    public class InvestigateState : IState
    {
        #region Fields
        private readonly StateContext _ctx;
        private readonly InvestigateConfig _config;
        #endregion

        #region Constructor
        public InvestigateState(StateContext context, InvestigateConfig config)
        {
            _ctx    = context;
            _config = config;
        }
        #endregion

        #region IState Implementation

        public void Enter()
        {

        }

        public void Tick()
        {
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
    }
}