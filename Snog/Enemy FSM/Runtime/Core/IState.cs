using UnityEngine;

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// Basic interface for FSM states
    /// </summary>
    public interface IState 
    {
        void Enter();
        void Tick();
        void Exit();    
    }
}