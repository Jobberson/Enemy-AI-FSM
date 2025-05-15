using UnityEngine;

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// Every state has access to this script 
    /// </summary>
    public class StateContext
    {
        // The enemy's transform.
        public Transform Owner { get; set; }

        // The player's transform 
        public Transform Player { get; set; }

        // These are scriptable objects for configurations
        public EnemyConfig EnemyConfig { get; set; }
        public WanderConfig WanderConfig { get; set; }
        public StalkConfig StalkConfig { get; set; }
        public ChaseConfig ChaseConfig { get; set; }
        public SearchConfig SearchConfig { get; set; }
        public RecoverConfig RecoverConfig { get; set; }

        // This is so that the states can access the movement scripts
        public AStarMovementController AStarController { get; set; }

        // These are interfaces for movement, vision and noise
        public IMovementController Movement { get; set; }
        public IVisionDetector Vision { get; set; }
        public INoiseDetector Noise { get; set; }

        // Runtime data
        public IState PreviousState { get; set; }
        public float TensionLevel { get; set; }
        public Vector3 LastKnownPosition { get; set; }
    }
}