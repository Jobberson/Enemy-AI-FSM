using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a random-ish wander behavior
    /// </summary>
    [CreateAssetmenu (
        fileName = "NewChaseConfig", 
        menuName = "Enemy FSM/Configs/Chase Config", 
        order = 4
    )]
    public class ChaseConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Max chase duration in seconds")] private float maxChaseDuration = 20;
        [SerializeField][Tooltip("Min chase duration in seconds")] private float minChaseDuration = 15;
        [SerializeField] private float catchDistance = 1.5f;

        private void OnValidate() 
        {
            // add clamps (max and min) here    
        }
    }
}