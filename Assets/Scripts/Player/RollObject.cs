using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollObject : MonoBehaviour
{
    public float torque = 10f;  // Adjust the torque value as needed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Apply torque to roll the object
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(Vector3.forward * torque);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(Vector3.back * torque);
        }
    }
}
