using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Global settings for the enemy bevahior.
    /// Shared across all states.
    /// (movement, detection, intensity inrcease).
    /// </summary>
    [CreateAssetmenu (
        fileName = "NewEnemyConfig", 
        menuName = "Enemy FSM/Configs/Enemy Config", 
        order = 0
    )]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Global Mulpliers")]
        [SerializeField][Tooltip("Global multiplier applied to speed")] 
        private float speedMultiplier = 1f;

        [SerializeField][Tooltip("Global multiplier for view distance")] 
        private float viewDistanceMultipler = 1f;

        [SerializeField][Tooltip("Global multiplier for hearing radius")] 
        private float hearingRadiusMultiplier = 1f;
        
        [Header("Intensity Increase")]
        [SerializeField][Tooltip("how quickly the enemy's aggressiveness rampsup over time")] 
        private AnimationCurve aggressionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private void OnValidate() 
        {
            // add clamps (max and min) here    
        }
    }
}