using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    public Rigidbody2D wheelRb;  // Rigidbody for the wheel
    public Rigidbody2D bodyRb;   // Rigidbody for the character's body
    
    [Space]
    [Header("Drive Parameters")]
    public float moveSpeed = 5f; 
    public float maxAngularVelocity = 500f; 
    public float directionChangeDamping = 2f;
    public float counterTorque = 100f; 

    [Space]
    public float balanceForce = 10f; 
    public float maxBalanceAngle = 15f;
    public float wobbleDamping = 2f; 

    [Space]
    public float friction = 1.5f; 

    [Space]
    [Header("Jump Parameters")]
    public bool shouldEnableJump = false;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float gravityScale = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float maxTiltAngle = 45f; // Max angle to allow jumps and auto-correct

    [Space]
    [Header("Character Change Parameters")]
    public CharacterManager characterManager;

    private float lastMoveInput = 0f;
    private bool isGrounded;

    private void Start()
    {
        // Set friction on the wheel's collider
        var wheelCollider = wheelRb.GetComponent<Collider2D>();
        if (wheelCollider != null)
        {
            var frictionMaterial = new PhysicsMaterial2D();
            frictionMaterial.friction = friction;
            frictionMaterial.bounciness = 0;
            wheelCollider.sharedMaterial = frictionMaterial;
        }

        bodyRb.gravityScale = gravityScale;
        wheelRb.gravityScale = gravityScale;
    }

    private void Update()
    {
        UpdateWheelMovement();
        UpdateJump();
        UpdateSize();
    }

    private void UpdateWheelMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        
        if (Mathf.Sign(moveInput) != Mathf.Sign(lastMoveInput) && moveInput != 0f)
        {
            wheelRb.angularVelocity = 0f; // Reset angular velocity
            wheelRb.AddTorque(-moveInput * counterTorque); 
        }

        if (Mathf.Abs(wheelRb.angularVelocity) < maxAngularVelocity)
        {
            wheelRb.AddTorque(-moveInput * moveSpeed);
        }

        float angle = bodyRb.rotation;

        if (Mathf.Abs(angle) < maxBalanceAngle)
        {
            float balanceInput = -angle / maxBalanceAngle; 
            bodyRb.AddTorque(balanceInput * balanceForce);
        }

        if (Mathf.Abs(angle) > 0.1f)
        {
            bodyRb.angularVelocity *= 1f / wobbleDamping;
        }

        wheelRb.rotation -= moveInput * moveSpeed * Time.deltaTime;

        lastMoveInput = moveInput;

        // Clamp the rotation to prevent tipping over
        ClampRotation();
    }

    private void ClampRotation()
    {
        float angle = bodyRb.rotation;

        // Auto-correct if the tilt angle is too large
        if (Mathf.Abs(angle) > maxTiltAngle)
        {
            float correctionTorque = -angle / maxTiltAngle * balanceForce;
            bodyRb.AddTorque(correctionTorque);
        }

        // Prevent the body from rotating beyond the tilt limits
        if (angle < -maxTiltAngle)
        {
            bodyRb.rotation = -maxTiltAngle;
        }
        else if (angle > maxTiltAngle)
        {
            bodyRb.rotation = maxTiltAngle;
        }
    }

    private void UpdateJump()
    {
        if (!shouldEnableJump)
        {
            return;
        }
        
        CheckGrounded();

        // Restrict jumping if the body is too tilted
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Mathf.Abs(bodyRb.rotation) < maxTiltAngle)
        {
            Action<Rigidbody2D> jumpAction = rb => rb.velocity += Vector2.up * jumpForce;
            ApplyToBothRigidBodies(jumpAction);
        }

        if (bodyRb.velocity.y < 0)
        {
            Action<Rigidbody2D> jumpActionOnFall = rb => rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            ApplyToBothRigidBodies(jumpActionOnFall);
        }
        else if (bodyRb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            Action<Rigidbody2D> jumpActionOnShortJump = rb => rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            ApplyToBothRigidBodies(jumpActionOnShortJump);
        }
    }

    private void ApplyToBothRigidBodies(Action<Rigidbody2D> action)
    {
        action?.Invoke(bodyRb);
        action?.Invoke(wheelRb);
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
