using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraPosition : MonoBehaviour
{
    public float speed;

    bool changePosition;
    bool changeRotation;

    Vector3 targetPosition;
    Quaternion targetRotation;

    void Awake()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if(transform.position != targetPosition)
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.rotation  != targetRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    public void ChangeCameraPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    public void ChangeCameraRotation(Quaternion newRotation)
    {
        targetRotation = newRotation;
    }
}
