using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour
{

    public float horizontalSpeed;
    public float horizontalAmplitude;

    public float verticalSpeed;
    public float verticalAmplitude;

    private Vector3 tempPosition;

    void Start()
    {
        tempPosition = transform.position;
    }

    void FixedUpdate()
    {
        tempPosition.x = Mathf.Cos(Time.realtimeSinceStartup * horizontalSpeed) * horizontalAmplitude;
        tempPosition.z = Mathf.Sin(Time.realtimeSinceStartup * horizontalSpeed) * horizontalAmplitude;

        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * verticalAmplitude;

        Vector3 relativePos = tempPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        transform.position = tempPosition;
        
    }
}
