using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a random-ish wander behavior
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewWanderConfig", 
        menuName = "Enemy FSM/Configs/Wander Config", 
        order = 1
    )]
    public class WanderConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Max duration in seconds of the wander state")] private float wanderDuration = 45f;
        [SerializeField][Tooltip("The radius where the enemy can choose a point to wander")] private float wanderCircleRadius = 30f;
        [SerializeField][Tooltip("How often will the enemy bias toward player location (percentage)")] private float biasProbaility = 60;
        [SerializeField][Tooltip("Accuracy radius for biasing toward the player")] private float biasAccuracy = 20f;

        public float WanderDuration => wanderDuration;
        public float WanderCircleRadius => wanderCircleRadius;
        public float biasProbaility => biasProbaility;
        public float BiasAccuracy => biasAccuracy;

        private void OnValidate()
        {
            wanderDuration = Mathf.Max(0f, wanderDuration);
            wanderCircleRadius = Mathf.Max(0f, wanderCircleRadius);
            biasProbaility = Mathf.Clamp(biasProbaility, 0f, 100f);
            biasAccuracy = Mathf.Max(0f, biasAccuracy);
        }
    }
}