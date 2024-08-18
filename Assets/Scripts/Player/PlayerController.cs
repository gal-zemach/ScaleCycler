using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rigidBody;
    public WheelCollider wheelCollider;
    
    [Space]
    public Transform wheelSpriteTransform;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Space]
    [Header("Parameters")]
    public float motorTorque = 100f;
    public float brakeTorque = 100f;
    public float maxSpeed = 5f;

    [Space]
    [Header("Controls")]
    public KeyCode JumpKey = KeyCode.W;

    private bool isGrounded;

    private Vector3 position;
    private Quaternion rotation;

    private float wheelInitialRadius;

    void Start()
    {
        wheelInitialRadius = wheelCollider.radius;
    }

    void Update()
    {
        UpdateWheelSprite();
    }

    void FixedUpdate()
    {
        CheckGrounded();

        // Calculate current speed in relation to the forward direction of the car
        // (this returns a negative number when traveling backwards)
        float forwardSpeed = Vector3.Dot(Vector3.right, rigidBody.velocity);

        // Calculate how close the car is to top speed
        // as a number from zero to one
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        // Use that to calculate how much torque is available 
        // (zero torque at top speed)
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        float moveInput = Input.GetAxisRaw("Horizontal"); // Returns -1, 0, or 1 for left, no input, or right
        if (moveInput != 0)
        {
            // Check whether the user input is in the same direction 
            // as the car's velocity
            bool isAccelerating = Mathf.Sign(moveInput) == Mathf.Sign(forwardSpeed) || Mathf.Abs(forwardSpeed) < 1f;
            if (isAccelerating)
            {
                wheelCollider.motorTorque = Mathf.Sign(moveInput) * currentMotorTorque;
                wheelCollider.brakeTorque = 0f;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                wheelCollider.brakeTorque = brakeTorque;
                wheelCollider.motorTorque = 0;
            }
            
            // Debug.Log($"Inpt: ({moveInput}); FwdSp: ({forwardSpeed}); SpFctr: ({speedFactor}); CurrMtrTrq:({currentMotorTorque})");
        }

        // Trigger environment jump when the spacebar is pressed and the player is grounded
        if (Input.GetKeyDown(JumpKey) && isGrounded)
        {
            // playerMover.Jump();
        }
    }

    void CheckGrounded()
    {
        // Check if the player is touching the ground
        isGrounded = Physics.OverlapSphere(groundCheck.position, 0.1f, groundLayer)?.Length > 0;
    }

    private void UpdateWheelSprite()
    {
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelSpriteTransform.position = position;
        wheelSpriteTransform.rotation = rotation;

        float wheelRadiusScale = wheelCollider.radius / wheelInitialRadius;
        wheelSpriteTransform.localScale = Vector3.one * wheelRadiusScale;
    }
}
