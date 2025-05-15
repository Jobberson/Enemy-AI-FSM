using UnityEngine;

namespace Snog.EnemyFSM.Configs
{
    /// <summary>
    /// Global configuration for all states:
    /// movement speeds, detection distances, and intensity limits.
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewEnemyConfig",
        menuName = "Enemy FSM/Configs/Enemy Config",
        order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Movement Speeds")]
        [Tooltip("Speed when wandering.")] public float wanderSpeed = 6f;
        [Tooltip("Speed when investigating noise.")] public float investigateSpeed = 8f;
        [Tooltip("Base speed when chasing.")] public float chaseSpeed = 10f;
        [Tooltip("Speed when recovering.")] public float recoverSpeed = 4f;

        [Header("Vision & Hearing")]
        [Tooltip("Max distance eyes can see.")] public float viewDistance = 10f;
        [Tooltip("Field of view angle (in degrees).")] public float viewAngle = 60f;
        [Tooltip("Seconds required to confirm sighting.")] public float timeToSpotPlayer = 1f;
        [Tooltip("Radius within which noises are detected.")] public float noiseDetectionRadius = 8f;
        
        [Header("Chase Lose Settings")]
        [Tooltip("Time (sec) to give up chase after losing sight.")] public float chaseLoseTime = 3f;
        
        [Header("Intensity Increase Limits")]
        [Tooltip("Min recover duration after intensity increase.")] public float minRecoverDuration = 5f;
        [Tooltip("Max chase duration allowed after intensity increase.")] public float maxChaseDurationCap = 25f;
        [Tooltip("Max min-chase duration allowed after intensity increase.")] public float minChaseDurationCap  = 20f;
        [Tooltip("Max view distance allowed after intensity increase.")] public float maxViewDistance = 20f;
        [Tooltip("Min time-to-spot-player allowed after intensity increase.")] public float minTimeToSpotPlayer = 0.5f;
        [Tooltip("Max wander speed allowed after intensity increase.")] public float maxWanderSpeed = 8f;
        [Tooltip("Max chase speed allowed after intensity increase.")] public float maxChaseSpeed = 12f;
        [Tooltip("Max investigate speed allowed after intensity increase.")] public float maxInvestigateSpeed = 10f;
        [Tooltip("Max bias chance (%) allowed after intensity increase.")] public float maxBiasChancePercentage = 75f;
        [Tooltip("Min bias accuracy allowed after intensity increase.")] public float maxBiasAccuracy = 10f;
        
        private void OnValidate()
        {
            wanderSpeed = Mathf.Max(0f, wanderSpeed);
            investigateSpeed = Mathf.Max(0f, investigateSpeed);
            chaseSpeed = Mathf.Max(0f, chaseSpeed);
            recoverSpeed = Mathf.Max(0f, recoverSpeed);

            viewDistance = Mathf.Max(0f, viewDistance);
            viewAngle = Mathf.Clamp(viewAngle, 0f, 360f);
            timeToSpotPlayer = Mathf.Max(0f, timeToSpotPlayer);
            noiseDetectionRadius = Mathf.Max(0f, noiseDetectionRadius);

            chaseLoseTime = Mathf.Max(0f, chaseLoseTime);

            minRecoverDuration = Mathf.Max(0f, minRecoverDuration);
            maxChaseDurationCap = Mathf.Max(0f, maxChaseDurationCap);
            minChaseDurationCap = Mathf.Max(0f, minChaseDurationCap);
            maxViewDistance = Mathf.Max(0f, maxViewDistance);
            minTimeToSpotPlayer = Mathf.Max(0f, minTimeToSpotPlayer);
            maxWanderSpeed = Mathf.Max(0f, maxWanderSpeed);
            maxChaseSpeed = Mathf.Max(0f, maxChaseSpeed);
            maxInvestigateSpeed = Mathf.Max(0f, maxInvestigateSpeed);
            maxBiasChancePercentage = Mathf.Clamp(maxBiasChancePercentage, 0f, 100f);
            maxBiasAccuracy = Mathf.Max(0f, maxBiasAccuracy);
        }
    }
}
