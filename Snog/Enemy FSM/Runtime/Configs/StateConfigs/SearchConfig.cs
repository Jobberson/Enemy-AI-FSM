using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings exclusively for the search state 
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewSearchConfig", 
        menuName = "Enemy FSM/Configs/Search Config", 
        order = 2
    )]
    public class SearchConfig : ScriptableObject
    {
        // The radius around the lastKnownPosition of the player that the enemy can choose a point to search around.
        [SerializeField][Tooltip("The radius where the enemy can choose a point to search")] private float searchCircleRadius = 15f;
        
        [SerializeField][Tooltip("Time to search for the player after losing sight")] private float searchDuration = 10f; 

        // accessors
        public float SearchCircleRadius => searchCircleRadius;
        public float SearchDuration => searchDuration;

        private void OnValidate()
        {
            searchCircleRadius = Mathf.Max(0f, searchCircleRadius);
            searchDuration = Mathf.Max(0f, searchDuration);
        }
    }
}