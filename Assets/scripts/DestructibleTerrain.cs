using UnityEngine;
using UnityEngine.U2D; // Required for SpriteShapeController and Spline

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
        else
        {
            Debug.LogError("[DestructibleTerrain] Missing SpriteShapeController on terrain!");
        }
    }

    public void MakeCrater(Vector2 worldHitPoint, float explosionRadius, int craterSegments = 5)
    {
        if (shapeController == null || spline == null) return;

        // 1. Convert world impact point to local terrain space
        Vector3 localImpact = transform.InverseTransformPoint(worldHitPoint);

        // 2. Remove existing spline points that fall inside the explosion circle
        for (int i = spline.GetPointCount() - 1; i >= 0; i--)
        {
            Vector3 pointPos = spline.GetPosition(i);
            if (Vector3.Distance(localImpact, pointPos) < explosionRadius)
            {
                spline.RemovePointAt(i);
            }
        }

        // 3. Find closest point index to insert crater vertices
        int insertIndex = GetClosestSplineIndex(localImpact);

        // 4. Insert new points forming a downward crater arc
        for (int i = 0; i <= craterSegments; i++)
        {
            float angle = Mathf.PI * (i / (float)craterSegments);
            
            Vector3 craterPoint = new Vector3(
                localImpact.x + Mathf.Cos(angle) * explosionRadius,
                localImpact.y - Mathf.Sin(angle) * explosionRadius,
                0f
            );

            spline.InsertPointAt(insertIndex + i, craterPoint);
            spline.SetTangentMode(insertIndex + i, ShapeTangentMode.Linear);
        }

        // 5. Rebuild visual shape and update PolygonCollider2D physics
        shapeController.RefreshShapePositions();
        
        Debug.Log($"[Terrain] Sprite Shape crater carved at local coordinates: {localImpact}");
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
