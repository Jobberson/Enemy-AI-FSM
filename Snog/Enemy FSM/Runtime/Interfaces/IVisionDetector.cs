using UnityEngine;

namespace Snog.EnemyFSM.Interfaces
{
    /// <summary>
    /// Abstracts Vision
    /// </summary>
    public interface IVisionDetector 
    {
        void CanSeePlayer();  
    }
}