using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 1.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestructibleTerrain terrain = collision.gameObject.GetComponent<DestructibleTerrain>();

        if (terrain != null)
        {
            Vector2 hitPoint = collision.contacts[0].point;
            terrain.MakeCrater(hitPoint, explosionRadius);
        }

        Destroy(gameObject);
    }
}
