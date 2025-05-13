using System;
using UnityEngine; 

namespace Snog.EnemyFSM.Core
{
    /// <summary>
    /// Generic, reusable finite state machine.
    /// Handles state transitions and ticking the active state.
    /// </summary>
    public class StateMachine<T> where T : IState
    {
        public T CurrentState { get; private set; }

        // Name of the current state (for debug display).
        public string CurrentStateName => CurrentState?.GetType().Name ?? "None";

        // Transitions to a new state.
        public void ChangeState(T newState)
        {
            if (newState == null) throw new ArgumentNullException(nameof(newState));
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        // Should be called every frame to update the active state.
        public void Tick()
        {
            CurrentState?.Tick();
        }
    }
}