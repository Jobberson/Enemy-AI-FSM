using UnityEngine.;

namespace Snog.EnemyFSM.Core 
{
    /// <summary>
    /// Builds each IState using the shared StateContext
    /// </summary>
    public class StateContext
    {
        public Transform Onwer { get; set; }
        public Transform Player { get; set; }
        public EnemyConfig EnemyConfig { get; set; }
        public IMovementController Movement { get; set; }
        public IVisionDetector Vision { get; set; }
        public INoiseDetection Noise { get; set; }
    }
}