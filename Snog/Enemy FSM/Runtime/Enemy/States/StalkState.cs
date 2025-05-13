using UnityEngine;

namespace Snog.EnemyFSM.Enemy.States 
{
    /// <summary>
    /// The enemy approahces the player but keeps his distance.
    /// </summary>
    public class StalkState : IState
    {
        private readonly StateContext _ctx;
        private float _timer;

        public StalkState(StateContext ctx) { _ctx = ctx; }

        public Enter() { }

        public Tick() { }

        public Exit() { }
    }
}