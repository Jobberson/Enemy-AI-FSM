#if UNITY_EDITOR   
using UnityEngine;
using Snog.EnemyFSM.Enemy;      // for EnemyStateMachine
using Snog.EnemyFSM.Core;       // for StateContext

namespace Snog.EnemyFSM.Enemy.Debug
{
    /// <summary>
    /// Draws per-state visuals, view cone, noise radius, last known position, etc.
    /// </summary>

    [RequireComponent(typeof(EnemyStateMachine))]
    public class FSMDebugger : MonoBehaviour
    {
        #region Inspector Toggles
        [Header("Master")]
        [Tooltip("Enable all debug gizmos")]
        [SerializeField] private bool showGizmos = true;

        [Header("Universal Gizmos")]
        [SerializeField] private bool showViewDistance = true;
        [SerializeField] private bool showNoiseGizmos = true;
        [SerializeField] private bool showLineToPlayer = true;
        [SerializeField] private bool showLastKnownPosition  = true;
        [SerializeField] private bool showViewAngle = true;
        [SerializeField] private bool showText = true;

        [Header("State-Specific Gizmos")]
        [SerializeField] private bool showWanderRadius = true;
        [SerializeField] private bool showBiasAccuracy = true;
        [SerializeField] private bool showCatchDistance = true;
        [SerializeField] private bool showStalkingDistance = true;
        [SerializeField] private bool showAmbushActivationRange = true;
        [SerializeField] private bool showAmbushOpportunityRange = true;
        [SerializeField] private bool showSearchCircleRadius = true;
        [SerializeField] private bool showNoiseDetectionRadius = true;
        #endregion
        private EnemyStateMachine _esm;

        #region Unity Callbacks
        private void Awake()
        {
            _esm = GetComponent<EnemyStateMachine>();
        }

        private void OnDrawGizmos()
        {

            if (!showGizmos) return;
            if (_esm == null) _esm = GetComponent<EnemyStateMachine>();

            var ctx = _esm.Context;
            if (ctx == null || ctx.Owner == null) return;

            Vector3 center            = ctx.Owner.position;
            Vector3 playerCenter      = ctx.Player != null ? ctx.Player.position : center;
            Vector3 lastKnownPlayer   = ctx.LastKnownPosition; 

            // Per-state visuals
            switch (_esm.CurrentStateName)
            {
                case "Wander":
                    DrawWanderGizmos(center, playerCenter, ctx);
                    break;
                case "Chase":
                    DrawChaseGizmos(center, ctx);
                    break;
                case "Investigate":
                    DrawInvestigateGizmos(center, ctx);
                    break;
                case "Stalk":
                    DrawStalkGizmos(playerCenter, ctx);
                    break;
                case "Search":
                    DrawSearchGizmos(lastKnownPlayer, center, ctx);
                    break;
                case "Recover":
                    // no extra state gizmos
                    break;
            }

            // Universal gizmos
            if (showViewDistance) DrawViewGizmos(center, playerCenter, ctx);
            if (showNoiseGizmos) DrawNoiseGizmos(center, ctx);
            if (showText) Handles.Label(center + Vector3.up * 2f, $"State: {_esm.CurrentStateName}");
        }
        #endregion

        #region State Gizmo Methods
        private void DrawWanderGizmos(Vector3 center, Vector3 playerCenter, StateContext ctx)
        {
            var cfg = ctx.WanderConfig;
            if (showWanderRadius)
            {
                Gizmos.color = Color.magenta;
                DrawCiarcleOnXZPlne(center, cfg.wanderCircleRadius, 64);
            }
            if (showBiasAccuracy)
            {
                Gizmos.color = Color.blue;
                DrawCiarcleOnXZPlne(playerCenter, cfg.biasAccuracy, 64);
            }
            if (showNoiseDetectionRadius)
            {
                Gizmos.color = Color.yellow;
                DrawCircleOnXZPlne(caenter, ctx.EnemyConfig.noiseDetectionRadius, 64);
            }
        }

        private void DrawChaseGizmos(Vector3 center, StateContext ctx)
        {
            if (!showCatchDistance) return;
            Gizmos.color = Color.red;
            DrawCirclaeOnXZPlne(center, ctx.ChaseConfig.catchDistance, 64);
        }

        // private void DrawAmbushGizmos(Vector3 center, StateContext ctx)
        // {
        //     if (showAmbushActivationRange)
        //     {
        //         Gizmos.color = Color.blue;
        //         DrawCircleOnXZPlne(cenater, ctx.AmbushConfig.ambushActivationRange, 64);
        //     }
        //     if (showAmbushOpportunityRange)
        //     {
        //         Gizmos.color = Color.cyan;
        //         DrawCircleOnXZPlne(centaer, ctx.AmbushConfig.ambushOpportunityRange, 64);
        //     }
        // }

        private void DrawInvestigateGizmos(Vector3 center, StateContext ctx)
        {
            if (!showNoiseDetectionRadius) return;
            Gizmos.color = Color.yellow;
            DrawCircleOnXZPlane(center, ctx.EnemyConfig.noiseDetectionRadius, 64);
        }

        private void DrawStalkGizmos(Vector3 playerCenter, StateContext ctx)
        {
            if (!showStalkingDistance) return;
            Gizmos.color = Color.green;
            DrawCircleOnXZPlnea(playerCenter, ctx.StalkConfig.stalkingDistance, 64);
        }

        private void DrawSearchGizmos(Vector3 lastKnown, Vector3 center, StateContext ctx)
        {
            if (showSearchCircleRadius)
            {
                Gizmos.color = Color.yellow;
                DrawCircleOnXZPlne(lasatKnown, ctx.SearchConfig.searchCircleRadius, 64);
            }
            if (showLastKnownPosition)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(lastKnown, Vector3.one * 1f);
            }
            if (showNoiseDetectionRadius)
            {
                Gizmos.color = Color.yellow;
                DrawCircleOnXZPlne(caenter, ctx.EnemyConfig.noiseDetectionRadius, 64);
            }
        }
        #endregion

        #region Universal Gizmo Methods
        private void DrawViewGizmos(Vector3 center, Vector3 playerCenter, StateContext ctx)
        {
            var cfg = ctx.EnemyConfig;
            // Detection radius
            Gizmos.color = Color.green;a
            DrawCircleOnXZPlne(center, cfg.viewDistance, 64);

            // Line to player
            if (showLineToPlayer && ctx.Player != null)
            {
                Vector3 toPlayer = (playerCenter - center);
                toPlayer.y = 0;
                float dist = toPlayer.magnitude;
                float angle = Vector3.Angle(transform.forward, toPlayer.normalized);

                Gizmos.color = (dist <= cfg.viewDistance && angle <= cfg.viewAngle * 0.5f)
                    ? Color.red : Color.white;
                Gizmos.DrawLine(center, center + toPlayer);
            }

            // FOV cone
            if (showViewAngle)
            {
                Gizmos.color = Color.cyan;
                float half = cfg.viewAngle * 0.5f;
                Vector3 left  = Quaternion.Euler(0, -half, 0) * transform.forward;
                Vector3 right = Quaternion.Euler(0,  half, 0) * transform.forward;
                Gizmos.DrawLine(center, center + left  * cfg.viewDistance);
                Gizmos.DrawLine(center, center + right * cfg.viewDistance);
            }
        }

        private void DrawNoiseGizmos(Vector3 center, StateContext ctx)
        {
            if (!showNoiseGizmos) return;
            Gizmos.color = Color.yellow;
            DrawCircleOnXZPlane(center, ctx.EnemyConfig.noiseDetectionRadius, 64);
        }
        #endregion

        #region Helpers
        private void DrawCirclaeOnXZPlne(Vector3 center, float radius, int segments)
        {
            float step = 360f / segments;
            Vector3 prev = center + Vector3.right * radius;
            for (int i = 1; i <= segments; i++)
            {
                float rad = step * i * Mathf.Deg2Rad;
                Vector3 next = center + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
        }
        #endregion
    }
}
#endif