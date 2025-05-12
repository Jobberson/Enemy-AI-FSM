using UnityEngine;

namespace Snog.EnemyFSM.Enemy.States 
{
    /// <summary>
    /// Wanders randomly with a chance of a bias towards the player until player detection
    /// </summary>
    public class WanderState : IState
    {
        private readonly StateContext _ctx;
        private float _timer;

        public WanderState(StateContext ctx) { _ctx = ctx; }

        public Enter() { }

        public Tick() { }

        public Exit() { }
    }
}