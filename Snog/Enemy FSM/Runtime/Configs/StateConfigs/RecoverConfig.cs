using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings for when the enemy stops search after a chase and recovers, 
    /// increasing the difficulty
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewRecoverConfig", 
        menuName = "Enemy FSM/Configs/Recover Config", 
        order = 3
    )]
    public class RecoverConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Duration in seconds of the recover state")] private float recoverDuration = 15f;

        public float RecoverDuration => recoverDuration;

        private void OnValidate() 
        {
            recoverDuration = Mathf.Max(0f, recoverDuration); 
        }
    }
}