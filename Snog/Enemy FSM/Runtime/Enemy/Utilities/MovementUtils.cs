using Pathfinding;
using UnityEngine;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Wraps setting speed and destination on A* Pathfinding components.
    /// </summary>
    public static class MovementUtils
    {
        /// <summary>
        /// Moves the AIPath towards target at given speed, via the AIDestinationSetter.
        /// </summary>
        public static void MoveTowards(
            AIPath aiPath,
            AIDestinationSetter destinationSetter,
            Transform moveTarget,
            Vector3 target,
            float speed)
        {
            aiPath.maxSpeed = speed;
            moveTarget.position = target;
            destinationSetter.target = moveTarget;
        }
    }
}
