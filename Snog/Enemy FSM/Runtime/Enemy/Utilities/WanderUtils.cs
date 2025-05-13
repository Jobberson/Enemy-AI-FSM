using UnityEngine;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Provides helper methods to the wander state.
    /// </summary>
    public static class WanderUtils
    {
        public static Vector3 GetNextWanderTarget(
            Vector3 origin,
            float radius,
            float biasChancePercent,
            float biasAccuracy,
            Vector3 playerPosition
        )
        {
            // 1. Roll to bias toward player
            if (Random.value * 100f < biasChancePercent)
            {
                // pick a point near player
                Vector2 rand2D = Random.insideUnitCircle * biasAccuracy;
                return playerPosition + new Vector3(rand2D.x, 0, rand2D.y);
            }
            // 2. Otherwise pick a random point around origin
            Vector2 circle = Random.insideUnitCircle * radius;
            return origin + new Vector3(circle.x, 0, circle.y);
        }
    }
}