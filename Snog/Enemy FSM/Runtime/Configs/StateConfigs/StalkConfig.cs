using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a a stalking-like behavior
    /// </summary>
    [CreateAssetmenu (
        fileName = "NewStalkConfig", 
        menuName = "Enemy FSM/Configs/Stalk Config", 
        order = 5
    )]
    public class StalkConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Max approach distance from the enemy to the player")] private float stalkingDistance = 15f;
        [SerializeField][Tooltip("Duration in seconds of the stalk state")] private float stalkDuration = 25f;

        private void OnValidate() 
        {
            // add clamps (max and min) here    
        }
    }
}