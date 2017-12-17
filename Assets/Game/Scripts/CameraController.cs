using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool multiplayer;
    public bool hideCursor;
    public Vector3 offset;
    public float smoothTime = 0.3F;

    Transform playerOne;
    Transform playerTwo;

    GameObject[] cursors;

    float leftPan;
    float rightPan;
    float upPan;
    float downPan;

    [HideInInspector]
    public Vector3[] panVectors;

    Camera mainCam;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        mainCam = Camera.main;

        playerOne = GameObject.Find("PlayerOne").transform;

        if (multiplayer)
            playerTwo = GameObject.Find("PlayerTwo").transform;

        cursors = GameObject.FindGameObjectsWithTag("Cursor");

        Cursor.visible = !hideCursor;

        leftPan = Screen.width * 0.2f;
        rightPan = Screen.width * 0.8f;
        upPan = Screen.height * 0.8f;
        downPan = Screen.height * 0.2f;

        panVectors = new Vector3[cursors.Length];
    }

    void Update()
    {
       // CheckForPan();
    }

    void LateUpdate()
    {
       // Pan();

        //if (!multiplayer)
        //{
        //    // transform.localRotation = Quaternion.identity;
        //    //transform.position = Vector3.SmoothDamp(transform.position, playerOne.transform.position + offset, ref velocity, smoothTime);
        //    transform.root.position = Vector3.Lerp(transform.position, playerOne.transform.position + offset, smoothTime);
        //}
        //else
        //{
        //    Vector3 targetLocation = new Vector3((playerOne.position.x + playerTwo.position.x) / 2, (playerOne.position.y + playerTwo.position.y) / 2, (playerOne.position.z + playerTwo.position.z) / 2);

        //    transform.position = Vector3.SmoothDamp(transform.position, targetLocation + offset, ref velocity, smoothTime);
        //    //transform.position = Vector3.Lerp(transform.position,targetLocation + offset, smoothTime * Time.deltaTime);
        //}
    }

    void CheckForPan()
    {
        Vector3[] cursorScreenPositions = new Vector3[cursors.Length];

        for(int i = 0; i < cursors.Length; i++)
        {
            cursorScreenPositions[i] = mainCam.WorldToScreenPoint(cursors[i].transform.position);

            if (cursorScreenPositions[i].x < leftPan)
            {
                panVectors[i].x = -1;
            }
            else if(cursorScreenPositions[i].x > rightPan)
            {
                panVectors[i].x = 1;
            }
            else
            {
                panVectors[i].x = 0;
            }

            if (cursorScreenPositions[i].y < downPan)
            {
                panVectors[i].y = -1;
            }
            else if (cursorScreenPositions[i].y > upPan)
            {
                panVectors[i].y = 1;
            }
            else
            {
                panVectors[i].y = 0;
            }
        }
    }

    void Pan()
    {
        if (!multiplayer)
            transform.position += new Vector3(panVectors[0].normalized.x * smoothTime * Time.deltaTime, panVectors[0].normalized.y * smoothTime * Time.deltaTime, 0);
        else
            transform.position += new Vector3((panVectors[0].normalized + panVectors[1].normalized).x * smoothTime * Time.deltaTime, (panVectors[0].normalized + panVectors[1].normalized).y * smoothTime * Time.deltaTime, 0);
    }

    /*
     * fix camera smooth when cursor is in corners
     * fix mouse pan getting fucked when you move slightly while in pan zone
     * 
     * detect if cursor is in the "pan" area, then pan
     * if both cursors are in opposing pan areas, the camera will move back(zoom out) - this will be clamped
     * as cursors move back towards each other, the camera will zoom in - this will be clamped
     */
}