using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool multiplayer;
    public Vector3 offset;
    public float smoothTime = 0.3F;

    Transform playerOne;
    Transform playerTwo;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerOne = GameObject.Find("PlayerOne").transform;

        if(multiplayer)
            playerTwo = GameObject.Find("PlayerTwo").transform;

    }

    void Update()
    {
        if(!multiplayer)
        {
            transform.LookAt(playerOne.transform.position);

            //transform.position = Vector3.Lerp(transform.position,  playerOne.transform.position + offset, Time.deltaTime * smoothTime);
            //transform.position = Vector3.SmoothDamp(transform.position,  playerOne.transform.position + offset, ref velocity, smoothTime);
        }
    }
}
