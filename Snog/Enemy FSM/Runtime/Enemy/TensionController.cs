using UnityEngine;
namespace Snog.EnemyFSM.Enemy
{
    /// <summary>
    /// Manages dynamic intensity over the enemy
    /// </summary>
    public class TensionController : MonoBehaviour
    {
        [SerializeField][Tooltip("Reference to the aggression curve in EnemyConfig")] EnemyConfig enemyConfig;
    
        private float _elapsedTime;

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            float tension = enemyConfig.aggressionCurve.Evaluate(_elapsedTime);

            // broadcast tension via event          
        }

        public float CurrentTension => enemyConfig.aggressionCurve.Evaluate(_elapsedTime);
    }
}