using UnityEngine; 

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// A resusable Finite State Machine bas class.
    /// Manages current state and handles transitions.
    /// </summary>
    public class StateMachine<TState> : MonoBehaviour where TState : IState
    {
        private TState _currentState;
        
        /// <summary>
        /// Changes the active state. 
        /// Calls Exit() on the old state and Enter() on the new state.
        /// </summary>
        public ChangeState(TState newState)
        {

        }

        /// <summary>
        /// Call this each frame to uptade the current state
        /// </summary>
        public void Tick()
        {

        }
    }
}