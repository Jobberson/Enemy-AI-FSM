using UnityEngine;
using Snog.EnemyFSM.Interfaces;
using Snog.EnemyFSM.Core;

namespace Snog.EnemyFSM.Enemy.Services
{
    /// <summary>
    /// Subscribes to global NoiseEventManager and forwards to context.
    /// </summary>
    [RequireComponent(typeof(EnemyStateMachine))]
    public class NoiseListener : MonoBehaviour, INoiseDetector
    {
        public event System.Action<Vector3, float> OnNoiseRaised;
        private EnemyStateMachine _esm;

        private void Awake()
        {
            _esm = GetComponent<EnemyStateMachine>();
            NoiseEventManager.OnNoise += HandleNoise;
        }

        private void OnDisable()
        {
            NoiseEventManager.OnNoise -= HandleNoise;
        }

        public void HandleNoise(Vector3 pos, float radius)
        {
            OnNoiseRaised?.Invoke(pos, radius);
        }
    }
}