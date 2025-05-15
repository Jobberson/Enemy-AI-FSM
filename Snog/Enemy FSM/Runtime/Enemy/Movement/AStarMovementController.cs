using UnityEngine;
using Pathfinding;
using Snog.EnemyFSM.Interfaces;

namespace Snog.EnemyFSM.Enemy.Movement
{
    /// <summary>
    /// IMovementController using A* Pathfinding Project components.
    /// </summary>
    [RequireComponent(typeof(AIPath), typeof(AIDestinationSetter))]
    public class AStarMovementController : MonoBehaviour, IMovementController
    {
        private AIPath _aiPath;
        private AIDestinationSetter _setter;
        private bool _stopped;

        private void Awake()
        {
            _aiPath = GetComponent<AIPath>();
            _setter = GetComponent<AIDestinationSetter>();
        }

        public void MoveTo(Vector3 destination, float speed)
        {
            _stopped = false;
            _aiPath.maxSpeed = speed;
            _setter.target.position = destination;
        }

        public void Stop()
        {
            _stopped = true;
            _aiPath.maxSpeed = 0f;
        }

        public bool IsAtDestination => _stopped || _aiPath.reachedEndOfPath;
    }
}