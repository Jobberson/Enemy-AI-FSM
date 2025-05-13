using UnityEngine;

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// Basic interface for FSM states
    /// </summary>
    public interface IState 
    {
        void Enter(); // acts as a start() for that state
        void Tick(); // acts as update() for that state
        void Exit(); // runs when exiting the state
    }
}