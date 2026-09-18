using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object hit has the DestructibleTerrain component
        DestructibleTerrain terrain = collision.gameObject.GetComponent<DestructibleTerrain>();

        if (terrain != null)
        {
            // Grab the exact contact point of the collision
            Vector2 hitPoint = collision.contacts[0].point;
            
            // Trigger the crater calculation on the terrain
            terrain.MakeCrater(hitPoint, explosionRadius);
            
            Debug.Log($"[Fireball Impact] Hit terrain at {hitPoint}");
        }

        // Destroy the fireball instance on impact
        Destroy(gameObject);
    }
}
