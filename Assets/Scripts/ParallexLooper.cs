using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallexLooper : MonoBehaviour
{
    [SerializeField] private float resetThreshold;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private Transform parallexItem1;
    [SerializeField] private Transform parallexItem2;

    private Vector3 startVector;
    private float distanceBetweenItems;

    void Awake()
    {
        distanceBetweenItems = Vector3.Distance(parallexItem1.position, parallexItem2.position);
        startVector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Mathf.Abs(startVector.x - playerTransform.position.x);

        if (distance > resetThreshold)
        {
            transform.position = startVector;
        }
    }
}
