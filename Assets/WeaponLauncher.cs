using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponLauncher : MonoBehaviour
{
    public GameObject missilePrefab;    // Drag your missile prefab here in Inspector
    public Transform firePoint;         // The tip of the barrel where the missile spawns
    public float launchPower = 15f;     // Firing power force

    void Update()
    {
        // Check if Spacebar is pressed down using the modern Input System
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FireMissile();
        }
    }

    void FireMissile()
    {
        // 1. Spawn the missile at the firePoint position and rotation
        GameObject newMissile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);

        // 2. Get the Rigidbody2D component from the spawned missile to apply physics
        Rigidbody2D rb = newMissile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 3. Apply a forward force in the direction the barrel is pointing
            rb.linearVelocity = firePoint.right * launchPower; 
        }
    }
}
