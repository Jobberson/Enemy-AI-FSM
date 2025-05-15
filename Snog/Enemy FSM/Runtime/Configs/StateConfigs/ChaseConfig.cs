using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings exclusive for the chase state
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewChaseConfig", 
        menuName = "Enemy FSM/Configs/Chase Config", 
        order = 4
    )]
    public class ChaseConfig : ScriptableObject
    {
        // These variables are for randominzing the chase duration.
        // The max will never be lower than the min and vice versa
        [SerializeField][Tooltip("Max chase duration in seconds")] private float maxChaseDuration = 20;
        [SerializeField][Tooltip("Min chase duration in seconds")] private float minChaseDuration = 15;
        
        [SerializeField] private float catchDistance = 1.5f;

        // accessors
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