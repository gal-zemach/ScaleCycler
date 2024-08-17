using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DumbConstraint : MonoBehaviour
{
    [SerializeField] private Transform transformToCopy;
    [SerializeField] private Vector3 constraintOffSet;

    public Vector3 _baseVector;

    void Awake()
    {
        if (transformToCopy == null)
        {
            enabled = false;
            Debug.LogError("Dumb constraint has null value");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transformToCopy.position + constraintOffSet;
    }
}
