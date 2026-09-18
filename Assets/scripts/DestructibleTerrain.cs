using UnityEngine;
using UnityEngine.U2D;

public class DestructibleTerrain : MonoBehaviour
{
    private SpriteShapeController shapeController;
    private Spline spline;

    private void Start()
    {
        shapeController = GetComponent<SpriteShapeController>();
        if (shapeController != null)
        {
            spline = shapeController.spline;
        }
    }

    public void MakeCrater(Vector2 worldHitPoint, float explosionRadius, int craterSegments = 5)
    {
        if (shapeController == null || spline == null) return;

        Vector3 localImpact = transform.InverseTransformPoint(worldHitPoint);

        // 1. Find the single closest point on the spline near the impact
        int closestIndex = GetClosestSplineIndex(localImpact);

        // 2. Instead of removing points and breaking the spline order,
        // offset nearby points downward to form a crater curve
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            Vector3 pointPos = spline.GetPosition(i);
            float dist = Vector3.Distance(localImpact, pointPos);

            if (dist < explosionRadius)
            {
                // Push existing points downward inside the blast radius
                float depth = (explosionRadius - dist);
                spline.SetPosition(i, new Vector3(pointPos.x, pointPos.y - depth, pointPos.z));
            }
        }

        // 3. Force Unity to rebuild the visual mesh and physics collider
        shapeController.BakeMesh();
        shapeController.BakeCollider();

        Debug.Log($"[Terrain System] Depressed spline near index {closestIndex} at local position: {localImpact}");
    }

    private int GetClosestSplineIndex(Vector3 targetPos)
    {
        int closestIndex = 0;
        float minDistance = float.MaxValue;

        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            float dist = Vector3.Distance(targetPos, spline.GetPosition(i));
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}