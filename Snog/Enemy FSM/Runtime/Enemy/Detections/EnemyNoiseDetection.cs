using UnityEngine;

namespace Snog.EnemyFSM.EnemyFSM
{
    /// <summary>
    /// Handles FOV checks and returns whether the player is visible or not
    /// </summary>
    public class EnemyVision : MonoBehaviour, INoiseDetector
    {
        [SerializeField][Tooltip("Max hearing radius")] private float hearingRadius;

        public bool HeardPlayerNoise(out Vector3 sourcePosition)
        {
            
        }
    }   
} 