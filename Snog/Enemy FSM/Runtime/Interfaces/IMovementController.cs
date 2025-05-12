using UnityEngine;

namespace Snog.EnemyFSM.Interfaces
{
    /// <summary>
    /// Abstracts movement (NavMesh, A* or custom).
    /// </summary>
    public interface IMovementController 
    {
        void MoveTo(Vector3 target, float speed);
        bool IsAtDestination [ get; ]     
    }
}