using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class ParallexEffect : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform[] parallexItems;
    [SerializeField] private float OffSetAmount;
    [SerializeField] private float OffSetScaler = 1;
    [SerializeField] private float _cacheXPos;
    public float daFuck;

    private Transform _cameraTransform;

    void Awake()
    {
        if (mainCamera != null)
        {
            _cameraTransform = mainCamera.GetComponent<Transform>();
            _cacheXPos = _cameraTransform.localPosition.x;
        }
    }

    void Update()
    {
        daFuck = _cameraTransform.localPosition.x;

        if (_cacheXPos > _cameraTransform.localPosition.x)
        {
            var counter = 0;

            foreach (var trn in parallexItems)
            {
                counter++;

                trn.localPosition = new Vector3(counter * OffSetAmount * OffSetScaler, trn.localPosition.z, 0);
            }

            _cacheXPos = _cameraTransform.localPosition.x;
        }
        else if (_cacheXPos < _cameraTransform.localPosition.x)
        {
            var counter = 0;

            foreach (var trn in parallexItems)
            {
                counter++;

                trn.localPosition = new Vector3(-(counter * OffSetAmount * OffSetScaler), trn.localPosition.z, 0);
            }

            _cacheXPos = _cameraTransform.localPosition.x;
        }
    }
}
