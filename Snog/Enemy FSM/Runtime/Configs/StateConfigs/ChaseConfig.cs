using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for the when the enemy is chasing the player after detection
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewChaseConfig", 
        menuName = "Enemy FSM/Configs/Chase Config", 
        order = 4
    )]
    public class ChaseConfig : ScriptableObject
    {
        
        /// <summary>
        /// Maximum time the enemy will chase the player before giving up.
        /// </summary>
        [SerializeField][Tooltip("Max chase duration in seconds")] private float maxChaseDuration = 20;
        
        /// <summary>
        /// Minimum time the enemy will chase the player before giving up.
        /// </summary>
        [SerializeField][Tooltip("Min chase duration in seconds")] private float minChaseDuration = 15;
        
        /// <summary>
        /// Distance that the enemy needs to be from the player to catch him 
        /// and trigger a game over
        /// </summary>
        [SerializeField] private float catchDistance = 1.5f;

        public float MaxChaseDuration => maxChaseDuration;
        public float MinChaseDuration => minChaseDuration;
        public float CatchDistance => catchDistance;

        private void OnValidate() 
        {   
            maxChaseDuration = Mathf.Max(0f, maxChaseDuration);
            minChaseDuration = Mathf.Clamp(minChaseDuration, 0f, maxChaseDuration);
            catchDistance = Mathf.Max(0f, catchDistance);
        }
    }
}