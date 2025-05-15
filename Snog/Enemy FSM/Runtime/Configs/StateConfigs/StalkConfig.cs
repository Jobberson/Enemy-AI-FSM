using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a stalking-like behavior
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewStalkConfig", 
        menuName = "Enemy FSM/Configs/Stalk Config", 
        order = 5
    )]
    public class StalkConfig : ScriptableObject
    {
        
        /// <summary>
        /// Maximum distance the enemy will maintain while stalking the player.
        /// </summary>
        [SerializeField][Tooltip("Max approach distance from the enemy to the player")] private float stalkingDistance = 15f;
        [SerializeField][Tooltip("Duration in seconds of the stalk state")] private float stalkDuration = 25f;

        public float StalkingDistance => stalkingDistance;
        public float StalkDuration => stalkDuration;

        private void OnValidate()
        {
            stalkingDistance = Mathf.Max(0f, stalkingDistance);
            stalkDuration = Mathf.Max(0f, stalkDuration);
        }
    }
}