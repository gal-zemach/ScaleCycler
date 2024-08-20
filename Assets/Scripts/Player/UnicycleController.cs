using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    public Rigidbody2D wheelRb;  // Rigidbody for the wheel
    public Rigidbody2D bodyRb;   // Rigidbody for the character's body
    
    [Header("Drive Parameters")]
    public float moveSpeed = 5f; 
    public float maxAngularVelocity = 500f; 
    public float counterTorque = 100f; 

    [Header("Balance Parameters")]
    public float balanceForce = 10f; 
    public float maxBalanceAngle = 15f;
    public float wobbleDamping = 2f; 

    [Header("Jump Parameters")]
    public bool shouldEnableJump = false;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float jumpForce = 10f;
    public float gravityScale = 5f;

    [Header("Character Change Parameters")]
    public CharacterManager characterManager;

    private float moveInput;
    private bool jumpInput;
    private bool isGrounded;

    private void Start()
    {
        bodyRb.gravityScale = gravityScale;
        wheelRb.gravityScale = gravityScale;
    }

    private void Update()
    {
        // Receive user input in Update
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);

        UpdateJump();

        // Handle size changes in Update
        UpdateSize();
    }

    private void FixedUpdate()
    {
        // Handle physics-related logic in FixedUpdate
        UpdateWheelMovement();
        UpdateBalance();
    }

    private void UpdateWheelMovement()
    {
        // Apply movement torque to the wheel
        if (Mathf.Abs(wheelRb.angularVelocity) < maxAngularVelocity)
        {
            wheelRb.AddTorque(-moveInput * moveSpeed);
        }

        // Apply counter-torque to stabilize direction changes
        if (Mathf.Sign(moveInput) != Mathf.Sign(wheelRb.angularVelocity) && moveInput != 0f)
        {
            wheelRb.angularVelocity = 0f; // Reset angular velocity
            wheelRb.AddTorque(-moveInput * counterTorque); 
        }
    }

    private void UpdateBalance()
    {
        float angle = bodyRb.rotation;

        // Apply balance torque based on the tilt angle
        float balanceInput = -angle / maxBalanceAngle;
        bodyRb.AddTorque(balanceInput * balanceForce);

        // Apply wobble damping
        if (Mathf.Abs(angle) > 0.1f)
        {
            bodyRb.angularVelocity *= 1f / wobbleDamping;
        }

        // Clamp the rotation to prevent excessive tilting
        if (angle < -maxBalanceAngle)
        {
            bodyRb.rotation = -maxBalanceAngle;
        }
        else if (angle > maxBalanceAngle)
        {
            bodyRb.rotation = maxBalanceAngle;
        }
    }

    private void UpdateJump()
    {
        if (!shouldEnableJump)
        {
            // Debug.Log("cant jump");
            return;
        }
        
        CheckGrounded();

        if (jumpInput && isGrounded)
        {
            // Debug.Log("jumping");
            bodyRb.velocity += Vector2.up * jumpForce;
            wheelRb.velocity += Vector2.up * jumpForce;
        }
        else if (jumpInput)
        {
            // Debug.Log("tried jumping");
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void UpdateSize()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncreaseSize();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            DecreaseSize();
        }
    }

    private void IncreaseSize()
    {
        characterManager.IncreaseSize();
    }

    private void DecreaseSize()
    {
        characterManager.DecreaseSize();
    }
}
