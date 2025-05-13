using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a random-ish wander behavior
    /// </summary>
    [CreateAssetmenu (
        fileName = "NewSearchConfig", 
        menuName = "Enemy FSM/Configs/Search Config", 
        order = 2
    )]
    public class SearchConfig : ScriptableObject
    {
        [SerializeField][Tooltip("The radius where the enemy can choose a point to search")] private float searchCircleRadius = 15f;
        [SerializeField][Tooltip("Time to search for the player after losing sight")] private float searchDuration = 10f; 

        private void OnValidate() 
        {
            // add clamps (max and min) here    
        }
    }
}