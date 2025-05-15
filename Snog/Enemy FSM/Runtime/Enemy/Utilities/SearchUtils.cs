using UnityEngine;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Provides search‐target selection around the player's last known position.
    /// </summary>
    public static class SearchUtils    
    {
        /// <summary>
        /// Picks a random point within searchCircleRadius around lastKnownPos.
        /// </summary>
        public static Vector3 GetNextSearchTarget(
            Vector3 lastKnownPos,
            float searchCircleRadius,
            float groundY)
        {
            // clamps radius to 150 to avoid extreme values
            searchCircleRadius = Mathf.Clamp(searchCircleRadius, 0.1f, 150f);

            Vector3 randomOffset = Random.insideUnitSphere * searchCircleRadius;
            randomOffset.y = 0;
            var searchPoint = lastKnownPos + randomOffset;
            searchPoint.y = groundY;
            return searchPoint;
        }
    }
}
