using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public bool multiplayer;
    public bool hideCursor;
    public Vector3 offset;
    public float smoothTime = 0.3F;

    public RectTransform reticle;

    public float rotationSpeed;

    public Vector3 upAngle = new Vector3(-22.5f, 0, 0);
    public Vector3 upRightAngle = new Vector3(-22.5f, 22.5f, -22.5f);
    public Vector3 upLeftAngle = new Vector3(-22.5f, -22.5f, 22.5f);

    public Vector3 downAngle = new Vector3(22.5f, 0, -0);
    public Vector3 downRightAngle = new Vector3(22.5f, 22.5f, -22.5f);
    public Vector3 downLeftAngle = new Vector3(22.5f, -22.5f, 22.5f);

    public Vector3 rightAngle = new Vector3(0, 22.5f, -22.5f);
    public Vector3 leftAngle = new Vector3(0, -22.5f, 22.5f);


    Transform playerOne;
    Transform playerTwo;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerOne = GameObject.Find("PlayerOne").transform;

        if (multiplayer)
            playerTwo = GameObject.Find("PlayerTwo").transform;

        if (hideCursor)
        {
            Cursor.visible = false;
        }
    }

    void Update()
    {
        CameraRotation();
        if (!multiplayer)
        {
            transform.position = Vector3.Lerp(transform.position, playerOne.transform.position + offset, smoothTime * Time.deltaTime);
        }
        else
        {
            Vector3 targetLocation = (playerOne.position + playerTwo.position) / 2;
            transform.position = Vector3.Lerp(transform.position, targetLocation + offset, smoothTime * Time.deltaTime);
        }
    }

    void CameraRotation()
    {
        Vector3 cursorVector = reticle.anchoredPosition;
        Vector3 screenSpace = Camera.main.ViewportToScreenPoint(cursorVector);

        float newAngle = Vector3.Angle(new Vector3(Screen.width, 0, 0), new Vector3(cursorVector.x, cursorVector.y, 0));

        if (screenSpace.y > Screen.height / 2)
        {
            if (newAngle <= 15)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rightAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 80 && newAngle > 15)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(upRightAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 100 && newAngle > 80)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(upAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 165 && newAngle > 100)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(upLeftAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 180)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(leftAngle), rotationSpeed * Time.deltaTime);
        }
        else if (screenSpace.y < Screen.height / 2)
        {
            if (newAngle <= 15)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rightAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 80 && newAngle > 15)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(downRightAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 100 && newAngle > 80)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(downAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 165 && newAngle > 100)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(downLeftAngle), rotationSpeed * Time.deltaTime);
            else if (newAngle <= 180)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(leftAngle), rotationSpeed * Time.deltaTime);
        }
    }
}
