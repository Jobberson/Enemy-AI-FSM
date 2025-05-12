using UnityEngine;

namespace Snog.EnemyFSM.EnemyFSM
{
    /// <summary>
    /// Handles FOV checks and returns whether the player is visible or not
    /// </summary>
    public class EnemyVision : MonoBehaviour, IVisionDetector
    {
        [SerializeField][Tooltip("Max view distance")] private float viewRadius;
        [SerializeField][Tooltip("FOV angle in degrees")] private float viwAngle;

        public bool CanSeePlayer(Transform player)
        {
            
        }
    }   
} 