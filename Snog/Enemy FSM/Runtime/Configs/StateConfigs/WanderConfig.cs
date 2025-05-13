using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a random-ish wander behavior
    /// </summary>
    [CreateAssetmenu (
        fileName = "NewWanderConfig", 
        menuName = "Enemy FSM/Configs/Wander Config", 
        order = 1
    )]
    public class WanderConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Max duration in seconds of the wander state")] private float wanderDuration = 45f;
        [SerializeField][Tooltip("The radius where the enemy can choose a point to wander")] private float wanderCircleRadius = 30f;
        [SerializeField][Tooltip("How often will the enemy bias toward player location (percentage)")] private float biasChancePercentage = 60;
        [SerializeField][Tooltip("Accuracy radius for biasing toward the player")] private float biasAccuracy = 20f;

        private void OnValidate() 
        {
            // add clamps (max and min) here    
        }
    }
}