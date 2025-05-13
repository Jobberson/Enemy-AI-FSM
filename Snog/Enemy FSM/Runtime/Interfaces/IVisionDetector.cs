using UnityEngine;

namespace Snog.EnemyFSM.Interfaces
{
    /// <summary>
    /// Abstracts Vision
    /// </summary>
    public interface IVisionDetector 
    {    
        bool CanSeeTarget(Transform target);
        Vector3 LastSeenPosition { get; }
    }
}