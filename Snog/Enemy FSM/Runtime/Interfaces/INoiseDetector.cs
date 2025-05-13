using UnityEngine;

namespace Snog.EnemyFSM.Interfaces
{
    /// <summary>
    /// Abstraction for noise detection.
    /// </summary>
    public interface INoiseDetector
    {
        // Subscribe to noise events in Awake(), unsubscribe in OnDisable().
        event System.Action<Vector3, float> OnNoiseRaised;

        // Called by NoiseEventManager when noise occurs.
        void HandleNoise(Vector3 position, float radius);
    }
}