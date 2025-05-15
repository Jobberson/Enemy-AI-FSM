using UnityEngine.;

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// Builds each IState using the shared StateContext
    /// </summary>
    public class StateContext
    {
        /// <summary>
        /// The enemy's transform.
        /// </summary>
        public Transform Owner { get; set; }

        /// <summary>
        /// The player's transform 
        /// </summary>
        public Transform Player { get; set; }

        /// <summary>
        /// These are scriptable objects for configurations
        /// </summary>
        public EnemyConfig EnemyConfig { get; set; }
        public WanderConfig WanderConfig { get; set; }
        public StalkConfig StalkConfig { get; set; }
        public ChaseConfig ChaseConfig { get; set; }
        public SearchConfig SearchConfig { get; set; }
        public RecoverConfig RecoverConfig { get; set; }

        /// <summary>
        /// This is so that the states can access the movement scripts
        /// </summary>
        public AStarMovementController AStarController { get; set; }

        /// <summary>
        /// These are interfaces for movement, vision and noise
        /// </summary>
        public IMovementController Movement { get; set; }
        public IVisionDetector Vision { get; set; }
        public INoiseDetector Noise { get; set; }

        // Runtime data
        public IState PreviousState { get; set; }
        public float TensionLevel { get; set; }
        public Vector3 LastKnownPosition { get; set; }
    }
}