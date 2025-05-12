using UnityEngine;

namespace Snog.EnemyFSM.Enemy.States 
{
    /// <summary>
    /// 
    /// </summary>
    public class RecoverState : IState
    {
        private readonly StateContext _ctx;
        private float _timer;

        public RecoverState(StateContext ctx) { _ctx = ctx; }

        public Enter() { }

        public Tick() { }

        public Exit() { }
    }
}