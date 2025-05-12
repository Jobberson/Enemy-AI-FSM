using UnityEngine;

namespace Snog.EnemyFSM.Enemy.States 
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchState : IState
    {
        private readonly StateContext _ctx;
        private float _timer;

        public SearchState(StateContext ctx) { _ctx = ctx; }

        public Enter() { }

        public Tick() { }

        public Exit() { }
    }
}