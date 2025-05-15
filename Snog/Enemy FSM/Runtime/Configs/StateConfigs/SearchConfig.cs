using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for when the enemy loses the player and the chase stops
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewSearchConfig", 
        menuName = "Enemy FSM/Configs/Search Config", 
        order = 2
    )]
    public class SearchConfig : ScriptableObject
    {
        /// <summary>
        /// The radius around the lastKnownPosition from the player 
        /// that the enemy can choose a point to search around.
        /// </summary>
        [SerializeField][Tooltip("The radius where the enemy can choose a point to search")] private float searchCircleRadius = 15f;
        [SerializeField][Tooltip("Time to search for the player after losing sight")] private float searchDuration = 10f; 

        public float SearchCircleRadius => searchCircleRadius;
        public float SearchDuration => searchDuration;

        private void OnValidate()
        {
            searchCircleRadius = Mathf.Max(0f, searchCircleRadius);
            searchDuration = Mathf.Max(0f, searchDuration);
        }
    }
}