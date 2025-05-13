using UnityEngine;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Handles “catch player” orientation and death trigger.
    /// </summary>
    public static class CombatUtils
    {
        /// <summary>
        /// Rotates toward player if within catchDistance + 5,
        /// and kills player if within catchDistance.
        /// </summary>
        public static void CatchPlayer(
            Transform enemyTransform,
            Transform playerTransform,
            float catchDistance)
        {
            float dist = Vector3.Distance(enemyTransform.position, playerTransform.position);

            if (dist <= catchDistance + 5f)
            {
                var dir = (playerTransform.position - enemyTransform.position).normalized;
                enemyTransform.rotation = Quaternion.LookRotation(dir);
            }

            if (dist <= catchDistance)
            {
                // Your game’s death logic:
                GameManager.Die();
            }
        }
    }
}
