using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for a random-ish wander behavior
    /// </summary>
    [CreateAssetmenu (
        fileName = "NewRecoverConfig", 
        menuName = "Enemy FSM/Configs/Recover Config", 
        order = 3
    )]
    public class RecoverConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Duration in seconds of the recover state")] private float recoverDuration = 15f;

        private void OnValidate() 
        {
            // add clamps (max and min) here    
        }
    }
}