using UnityEngine;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Handles “catch player” orientation and death trigger.
    /// </summary>
    public static class CombatUtils
    {
        /// <summary>
        /// Rotates smoothly toward player if within catchDistance + buffer,
        /// and kills player if within catchDistance.
        /// </summary>
        public static void CatchPlayer(
            Transform enemyTransform,
            Transform playerTransform,
            float catchDistance,
            float rotationBuffer = 5f,
            float rotationSpeed = 5f)
        {
            float dist = Vector3.Distance(enemyTransform.position, playerTransform.position);

            if (dist <= catchDistance + rotationBuffer)
            {
                Vector3 direction = (playerTransform.position - enemyTransform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyTransform.rotation = Quaternion.RotateTowards(
                    enemyTransform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            if (dist <= catchDistance)
            {
                GameManager.Die(); // Replace with your actual death logic
                // I suggest using a game manager with static methods
            }
        }
    }
}
