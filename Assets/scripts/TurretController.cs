using UnityEngine;
using UnityEngine.InputSystem;
public class TurretController : MonoBehaviour
{
    [Header("Turret Rotation")]
    [SerializeField] private float currentAngle = 0f;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float angleStep = 10f;

    [Header("Shooting References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float launchForce = 20f;

    private void Start()
    {
        ApplyRotation();
    }

    private void Update()
    {
        // Increase angle (rotate up/left) with Right Arrow
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentAngle = Mathf.Clamp(currentAngle + angleStep, minAngle, maxAngle);
            ApplyRotation();
        }

        // Decrease angle (rotate down/right) with Left Arrow
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentAngle = Mathf.Clamp(currentAngle - angleStep, minAngle, maxAngle);
            ApplyRotation();
        }

        // Fire projectile with Enter key (Main Enter or Numpad Enter)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Shoot();
        }
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        // Instantiate projectile at FirePoint with current turret rotation
        GameObject spawnedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Apply force along the direction the barrel is pointing
        Rigidbody2D rb = spawnedProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.right * launchForce, ForceMode2D.Impulse);
        }
        Destroy(spawnedProjectile, 3f); // Destroy the projectile after 5 seconds to prevent clutter
    }
}
