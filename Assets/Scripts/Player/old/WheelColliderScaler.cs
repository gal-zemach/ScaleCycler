using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelColliderScaler : MonoBehaviour
{
    public WheelCollider wheelCollider;    
    public float scaleSpeed = 0.1f; // Speed of scaling
    public float minRadius = 2f;
    public float maxRadius = 6f;

    private Vector3 initialMousePosition;
    private float initialRadius;
    private bool isScaling;

    void Update()
    {
        transform.position = wheelCollider.transform.position + new Vector3(wheelCollider.radius, wheelCollider.radius, 0);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && isScaling)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = 0; // Ensure we're working in 2D

            Vector3 mouseDelta = currentMousePosition - initialMousePosition;
            float newRadius = initialRadius + MaxByAbs(mouseDelta.x, mouseDelta.y) * scaleSpeed;

            // Apply the new scale and position
            wheelCollider.radius = Mathf.Clamp(newRadius, minRadius, maxRadius);
        }
    }

    void OnMouseDown()
    {
        // Debug.Log("mouse down on dot");

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // Ensure we're working in 2D

        initialMousePosition = mouseWorldPosition;
        initialRadius = wheelCollider.radius;
        isScaling = true;
    }

    void OnMouseUp()
    {
        // Debug.Log("mouse up on dot");

        isScaling = false;
    }

    private float MaxByAbs(float a, float b)
    {
        if (Mathf.Abs(a) < Mathf.Abs(b))
        {
            return b;
        }

        return a;
    }
}
