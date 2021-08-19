using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchyLookAt : MonoBehaviour
{
    Transform _myTF;

    [Tooltip("This value will be divided by 1 which would be used in Lerp")]
    [Range(0, 100)]
    [SerializeField] private float rotationSpd = 0.1f;

    [SerializeField] private Transform point;

    void Awake()
    {
        _myTF = transform;
    }


    void Update()
    {

        if(point != null) { 
        Vector3 direction = point.position - _myTF.position;
        Quaternion toRotation = Quaternion.LookRotation(transform.forward, direction);
            _myTF.rotation = Quaternion.Lerp(_myTF.rotation, toRotation, rotationSpd * Time.time);
        }
    }

}
