using UnityEngine;

namespace Snog.EnemyFSM.Enemy.States 
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaseState : IState
    {
        private readonly StateContext _ctx;
        private float _timer;

        public ChaseState(StateContext ctx) { _ctx = ctx; }

        public Enter() { }

        public Tick() { }

        public Exit() { }
    }
}