using UnityEngine;
using Snog.EnemyFSM.Core;

namespace Snog.EnemyFSM.Enemy.Utils
{
    /// <summary>
    /// Handles timed confirmation of seeing or losing sight of the player.
    /// </summary>
    public static class SightUtils
    {
        /// <summary>
        /// Returns true once the player has been in sight for at least timeToSpotPlayer seconds.
        /// </summary>
        public static bool CheckPlayerSight(StateContext ctx, ref float visibleTimer)
        {
            if (ctx.Vision.CanSeePlayer(ctx.Player))
            {
                visibleTimer = Mathf.Min(visibleTimer + Time.deltaTime, ctx.EnemyConfig.timeToSpotPlayer + 1);
                
                if (visibleTimer >= ctx.EnemyConfig.timeToSpotPlayer)
                {
                    #if UNITY_EDITOR
                    Debug.Log($"Player visible for {visibleTimer} seconds.");
                    #endif
                    
                    visibleTimer = 0f;
                    return true;
                }
            }
            else
            {
                visibleTimer = 0f;
            }
            return false;
        }

        /// <summary>
        /// Returns true once the player has been out of sight for at least chaseLoseTime seconds.
        /// </summary>
        public static bool CheckLosePlayerSight(StateContext ctx, ref float loseTimer)
        {
            if (!ctx.Vision.CanSeePlayer(ctx.Player))
            {
                loseTimer += Time.deltaTime;
                if (loseTimer >= ctx.EnemyConfig.chaseLoseTime)
                {
                    loseTimer = 0f;
                    return true;
                }
            }
            else
            {
                loseTimer = 0f;
            }
            return false;
        }
    }
}
