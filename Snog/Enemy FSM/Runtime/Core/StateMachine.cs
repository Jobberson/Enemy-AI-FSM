using System;
using UnityEngine; 

namespace Snog.EnemyFSM.Core
{
    /// <summary>
    /// The finite state machine.
    /// Handles state transitions and updating the active state every frame.
    /// </summary>
    public class StateMachine<T> where T : IState
    {
        public T CurrentState { get; private set; }
        public IState PreviousState { get; private set; }

        // Name of the current state (to display it on the inspector only).
        public string CurrentStateName => CurrentState?.GetType().Name ?? "None";

        // Transitions to a new state.
        public void ChangeState(T newState)
        {
            if (newState == null) throw new ArgumentNullException(nameof(newState));
            PreviousState = CurrentState; // caches the previous state
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