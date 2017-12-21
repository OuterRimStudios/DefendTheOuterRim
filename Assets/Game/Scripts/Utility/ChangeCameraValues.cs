using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraValues : MonoBehaviour
{
    public Vector3 desiredPosition;
    public Vector3 desiredRotation;

    AdjustCameraPosition adjustCamera;

    void Start()
    {
        adjustCamera = Camera.main.GetComponent<AdjustCameraPosition>();
    }

    public void ChangeCameraPosition()
    {
        adjustCamera.ChangeCameraPosition(desiredPosition);
    }

    public void ChangeCameraRotation()
    {
        adjustCamera.ChangeCameraRotation(Quaternion.Euler(desiredRotation));
    }
}
