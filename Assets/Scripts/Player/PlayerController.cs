using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerMover environmentMover;

    public Transform groundCheck;
    public LayerMask groundLayer;

    [Space]
    [Header("Controls")]
    // public KeyCode ForwardsKey = KeyCode.D;
    // public KeyCode BackwardsKey = KeyCode.A;
    public KeyCode JumpKey = KeyCode.W;

    private bool isGrounded;

    void Update()
    {
        if (environmentMover == null)
        {
            return;
        }
        
        CheckGrounded();

        // Handle horizontal movement based on input
        float moveInput = Input.GetAxisRaw("Horizontal"); // Returns -1, 0, or 1 for left, no input, or right
        if (moveInput != 0)
        {
            environmentMover.MoveHorizontal(moveInput);
        }

        // Trigger environment jump when the spacebar is pressed and the player is grounded
        if (Input.GetKeyDown(JumpKey) && isGrounded)
        {
            environmentMover.Jump();
        }

        // // Example: Change the shape of the wheel
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     ChangeWheelShape(1);
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     ChangeWheelShape(2);
        // }
    }

    void CheckGrounded()
    {
        // Check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    // void ChangeWheelShape(int shapeIndex)
    // {
    //     // Implement your logic to change the wheel's shape here
    //     Debug.Log("Wheel shape changed to: " + shapeIndex);
    // }
}
