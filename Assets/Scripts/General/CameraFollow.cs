using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothTime = 0.3f; // Time to smooth the camera's movement
    private float minX; // Minimum X position the camera can move to
    private Vector3 velocity = Vector3.zero; // Velocity of the camera, used by SmoothDamp

    void Start()
    {
        // Set the initial minimum X position to the camera's starting position
        minX = transform.position.x;
    }

    void LateUpdate()
    {
        // Calculate the target position for the camera (fixed y position)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // Ensure the camera's X position does not move left beyond minX
        float clampedX = Mathf.Max(targetPosition.x, minX);
        targetPosition.x = clampedX;

        // Smoothly move the camera towards the target position using SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Update minX if the player moves to the right
        if (targetPosition.x > minX)
        {
            minX = targetPosition.x;
        }
    }
}
