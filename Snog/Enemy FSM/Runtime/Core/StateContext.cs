using UnityEngine.;

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// Builds each IState using the shared StateContext
    /// </summary>
    public class StateContext
    {
        public Transform Owner { get; set; }
        public Transform Player { get; set; }

        public EnemyConfig EnemyConfig { get; set; }
        public WanderConfig WanderConfig { get; set; }
        public StalkConfig StalkConfig { get; set; }
        public ChaseConfig ChaseConfig { get; set; }
        public SearchConfig SearchConfig { get; set; }
        public RecoverConfig RecoverConfig { get; set; }

        public IMovementController Movement { get; set; }
        public IVisionDetector Vision { get; set; }
        public INoiseDetector Noise { get; set; }

        // Runtime data
        public IState PreviousState { get; set; }
        public float TensionLevel { get; set; }
        public Vector3 LastKnownPosition { get; set; }
    }
}