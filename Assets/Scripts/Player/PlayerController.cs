using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerMover playerMover;

    public Transform groundCheck;
    public LayerMask groundLayer;

    [Space]
    [Header("Controls")]
    public KeyCode JumpKey = KeyCode.W;

    private bool isGrounded;

    private Vector2 mouseDragStartPos;
    private Vector2 mouseDragEndPos;

    void Update()
    {
        CheckGrounded();

        // Handle horizontal movement based on input
        float moveInput = Input.GetAxisRaw("Horizontal"); // Returns -1, 0, or 1 for left, no input, or right
        if (moveInput != 0)
        {
            playerMover.MoveHorizontal(moveInput);
        }

        // Trigger environment jump when the spacebar is pressed and the player is grounded
        if (Input.GetKeyDown(JumpKey) && isGrounded)
        {
            playerMover.Jump();
        }
    }

    void CheckGrounded()
    {
        // Check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
