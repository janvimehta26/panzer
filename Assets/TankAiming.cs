using UnityEngine;
// 1. We must include the new Input System library at the top
using UnityEngine.InputSystem; 

public class TankAiming : MonoBehaviour
{
    public float rotationSpeed = 90f; 
    private float currentAngle = 45f; 

    void Update()
    {
        float input = 0f;

        // 2. Use the modern Input System to check if keys are pressed down
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                input = -1f; // Rotate left
            }
            else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                input = 1f; // Rotate right
            }
        }

        // 3. Keep the exact same math calculation from before
        currentAngle -= input * rotationSpeed * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, 0f, 180f);

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}

