using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings exclusively for the stalk state
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewStalkConfig", 
        menuName = "Enemy FSM/Configs/Stalk Config", 
        order = 5
    )]
    public class StalkConfig : ScriptableObject
    {
        // Maximum distance the enemy will maintain from the player while stalking.
        [SerializeField][Tooltip("Max approach distance from the enemy to the player")] private float stalkingDistance = 15f;
        
        [SerializeField][Tooltip("Duration in seconds of the stalk state")] private float stalkDuration = 25f;

        // accessors
        public float StalkingDistance => stalkingDistance;
        public float StalkDuration => stalkDuration;

        private void OnValidate()
        {
            stalkingDistance = Mathf.Max(0f, stalkingDistance);
            stalkDuration = Mathf.Max(0f, stalkDuration);
        }
    }
}