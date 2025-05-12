using UnityEngine;

namespace Snog.EnemyFSM.Enemy.States 
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestigateState : IState
    {
        private readonly StateContext _ctx;
        private float _timer;

        public InvestigateState(StateContext ctx) { _ctx = ctx; }

        public Enter() { }

        public Tick() { }

        public Exit() { }
    }
}