using UnityEngine;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Handles IncreaseIntensity logic from original AI.
    /// </summary>
    public static class TensionService
    {
        public static void IncreaseIntensity(
            ref float recoverDuration,
            ref float maxChaseDuration,
            ref float minChaseDuration,
            ref float timeToSpotPlayer,
            ref float viewDistance,
            ref float wanderSpeed,
            ref float chaseSpeed,
            ref float investigateSpeed,
            ref float biasChancePercentage,
            ref float biasAccuracy,
            float minRecoverDuration,
            float maxMaxChaseDuration,
            float maxMinChaseDuration,
            float minTimeToSpotPlayer,
            float maxViewDistance,
            float maxWanderSpeed,
            float maxChaseSpeed,
            float maxInvestigateSpeed,
            float maxBiasChancePercentage,
            float maxBiasAccuracy)
        {
            recoverDuration       = Mathf.Max(recoverDuration       - 2f, minRecoverDuration);
            maxChaseDuration      = Mathf.Min(maxChaseDuration      + 2f, maxMaxChaseDuration);
            minChaseDuration      = Mathf.Min(minChaseDuration      + 2f, maxMinChaseDuration);
            timeToSpotPlayer      = Mathf.Max(timeToSpotPlayer      - 0.1f, minTimeToSpotPlayer);
            viewDistance          = Mathf.Min(viewDistance          + 2f, maxViewDistance);
            wanderSpeed           = Mathf.Min(wanderSpeed           + 0.5f, maxWanderSpeed);
            chaseSpeed            = Mathf.Min(chaseSpeed            + 0.2f, maxChaseSpeed);
            investigateSpeed      = Mathf.Min(investigateSpeed      + 0.5f, maxInvestigateSpeed);
            biasChancePercentage  = Mathf.Min(biasChancePercentage  + 2f, maxBiasChancePercentage);
            biasAccuracy          = Mathf.Min(biasAccuracy          - 1f, maxBiasAccuracy);
        }
    }
}