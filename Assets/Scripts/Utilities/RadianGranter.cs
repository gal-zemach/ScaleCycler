using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RadianGranter : MonoBehaviour
{

    [SerializeField] private Transform[] transformeez;
    private Vector3 _cacheRadians;
    //private Transform 
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_cacheRadians != transform.localEulerAngles)
        {
            _cacheRadians = transform.localEulerAngles;
            foreach (var trn in transformeez)
            {
                trn.localEulerAngles += new Vector3(0, 0, trn.localEulerAngles.z + 0.1f);
            }
        }


    }
}
