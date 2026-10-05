using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    [Header("Facing Setup")]
    // Set to 1 for Player 1 (Purple), -1 for Player 2 (Yellow)
    [SerializeField] private float facingDirection = 1f; 

    [Header("Turret Rotation")]
    [SerializeField] private float currentAngle = 0f;
    [SerializeField] private float minAngle = -80f;
    [SerializeField] private float maxAngle = 80f;
    [SerializeField] private float angleStep = 10f;

    [Header("Shooting References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float launchForce = 20f;

    private void Update()
    {
        if (!enabled || Keyboard.current == null) return;

        // Up Arrow aims higher
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            currentAngle = Mathf.Clamp(currentAngle + angleStep, minAngle, maxAngle);
            ApplyRotation();
        }

        // Down Arrow aims lower
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            currentAngle = Mathf.Clamp(currentAngle - angleStep, minAngle, maxAngle);
            ApplyRotation();
        }

        // Fire shell
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle * facingDirection);
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject spawnedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 launchVector = firePoint.right * facingDirection;
            rb.AddForce(launchVector * launchForce, ForceMode2D.Impulse);
        }

        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.OnFire();
        }
    }
}