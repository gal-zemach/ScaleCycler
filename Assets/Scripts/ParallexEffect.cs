using UnityEngine;

[ExecuteInEditMode]
public class ParallaxEffect : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float parallaxFactor = 0.5f;  // How much parallax to apply (0 means no movement, 1 means full movement)

    private Vector3 previousPlayerPosition;

    void Start()
    {
        if (player == null)
        {
            player = Camera.main.transform;  // If no player, use the main camera
        }

        // Store the initial player position
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        // Calculate how far the player has moved since the last frame
        float deltaX = player.position.x - previousPlayerPosition.x;

        // Move the background at a reduced rate depending on the parallax factor
        transform.position += new Vector3(deltaX * parallaxFactor, 0, 0);

        // Update the previous player position
        previousPlayerPosition = player.position;
    }
}
