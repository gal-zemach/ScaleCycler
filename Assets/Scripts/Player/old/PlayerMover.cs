using UnityEngine;

public class PlayerMover : MonoBehaviour
{    
    public Rigidbody2D rb;

    [Space]
    [Header("Movement Parameters")]
    public float MoveForce = 50f;
    public float JumpForce = 300f;

    void Start()
    {
        rb.gravityScale = 2f; // Adjust gravity scale if needed for a better effect
    }

    public void MoveHorizontal(float moveInput)
    {
        rb.AddForce(new Vector2(moveInput * MoveForce, 0));
    }

    public void Jump()
    {
        if (rb.velocity.y == 0) // Ensure the environment is grounded before jumping
        {   
            rb.AddForce(Vector2.up * JumpForce);
        }
    }
}
