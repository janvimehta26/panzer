using UnityEngine;

public class TankAiming : MonoBehaviour
{
    public float rotationSpeed = 90f; // Degrees per second
    private float currentAngle = 45f; // Start pointing slightly up

    void Update()
    {
        // Get input from A/D keys or Left/Right Arrow keys
        float input = Input.GetAxis("Horizontal");

        // Calculate and clamp the angle so the barrel can't point into the ground
        currentAngle -= input * rotationSpeed * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, 0f, 180f);

        // Apply rotation around the Z-axis for 2D
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}