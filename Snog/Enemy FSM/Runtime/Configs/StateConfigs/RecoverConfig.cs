using UnityEngine;

namespace Snog.EnemyFSM.Configs 
{
    /// <summary>
    /// Settings exclusive for the recover state
    /// Note that after recovery the intensity or the enemy increases
    /// You can tweak the limits at "./EnemyConfig.cs"
    /// and you can tweak how much they increase at "../Enemy/Utilities/TensionUtils.cs"
    /// </summary>
    [CreateAssetMenu (
        fileName = "NewRecoverConfig", 
        menuName = "Enemy FSM/Configs/Recover Config", 
        order = 3
    )]
    public class RecoverConfig : ScriptableObject
    {
        [SerializeField][Tooltip("Duration in seconds of the recover state")] private float recoverDuration = 15f;

        // accessor
        public float RecoverDuration => recoverDuration;

        private void OnValidate() 
        {
            recoverDuration = Mathf.Max(0f, recoverDuration); 
        }
    }
}