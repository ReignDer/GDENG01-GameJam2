using UnityEngine;

public class RotateSpotlight : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotate around the Z axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
