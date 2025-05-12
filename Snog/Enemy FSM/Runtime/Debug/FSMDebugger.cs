using UnityEngine;

namespace Snog.EnemyFSM.Debug
{
    /// <summary>
    /// Draws runtime gizmos for vision cones, ranges, state transitions and targets
    /// </summary>
    public class FSMDebugger : MonoBehaviour
    {

        // helper function to draw a circle instead of using spheres
        private void DrawCircleXZ(Vector3 center, float radius, int segments)
        {
            float angleStep = 360f / segments;
            Vector3 prevPoint = center + new Vector3(radius, 0, 0);
            for (int i = 1; i <= segments; i++)
            {
                float rad = angleStep * i * Mathf.Deg2Rad;
                Vector3 nextPoint = center + new Vector3(Mathf.Cos(rad) * radius, 0, Mathf.Sin(rad) * radius);
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
    }
}