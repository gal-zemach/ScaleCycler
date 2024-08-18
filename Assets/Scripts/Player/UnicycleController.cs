using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    public Rigidbody2D wheelRb;  // Rigidbody for the wheel
    public Rigidbody2D bodyRb;   // Rigidbody for the character's body
    public float moveSpeed = 5f; // Speed of the unicycle movement
    public float balanceForce = 10f; // Force applied to balance the unicycle
    public float maxBalanceAngle = 15f; // Maximum tilt angle before losing balance
    public float maxAngularVelocity = 500f; // Maximum spinning speed of the wheel
    public float directionChangeDamping = 2f; // Damping effect when changing direction
    public float counterTorque = 100f; // Torque applied when changing direction
    public float friction = 1.5f; // Friction to reduce sliding

    private float lastMoveInput = 0f;

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
    }

    private void Update()
    {
        // Movement input
        float moveInput = Input.GetAxis("Horizontal");

        // Apply damping and counter-torque when changing direction
        if (Mathf.Sign(moveInput) != Mathf.Sign(lastMoveInput) && moveInput != 0f)
        {
            wheelRb.angularVelocity = 0f; // Reset angular velocity
            wheelRb.AddTorque(-moveInput * counterTorque); // Apply stronger counter-torque
        }

        // Limit the wheel's spinning speed
        if (Mathf.Abs(wheelRb.angularVelocity) < maxAngularVelocity)
        {
            wheelRb.AddTorque(-moveInput * moveSpeed);
        }

        // Get the angle of the unicycle body
        float angle = bodyRb.rotation;

        // Apply balancing force
        if (Mathf.Abs(angle) < maxBalanceAngle)
        {
            float balanceInput = -angle / maxBalanceAngle; // Normalize angle to a value between -1 and 1
            bodyRb.AddTorque(balanceInput * balanceForce);
        }

        // Adjust wheel rotation visually (optional)
        wheelRb.rotation -= moveInput * moveSpeed * Time.deltaTime;

        lastMoveInput = moveInput;
    }
}
