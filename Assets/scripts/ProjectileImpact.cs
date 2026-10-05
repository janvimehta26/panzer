using UnityEngine;

public class ProjectileImpact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestructibleTerrain terrain = collision.gameObject.GetComponent<DestructibleTerrain>();

        if (terrain != null)
        {
            Vector2 hitPoint = collision.contacts[0].point;
            terrain.MakeCrater(hitPoint, 3.0f);
        }

        // Trigger turn swap in TurnManager
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.EndTurn();
        }

        Destroy(gameObject);
    }
}
