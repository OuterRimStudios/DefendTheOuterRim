using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public float speed;
    Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    public void MoveCursor(float movex, float movey, bool hasMouse)
    {
        if (!hasMouse)
        {
            float moveX = movex * speed * Time.deltaTime;
            float moveY = movey * speed * Time.deltaTime;
            transform.position += new Vector3(moveX, moveY, 0f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, Screen.width * .2f, Screen.width * .8f), Mathf.Clamp(transform.position.y, Screen.height * .2f, Screen.height * .8f), mainCam.nearClipPlane);
        }
        else
        {
            transform.position = Input.mousePosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, Screen.width * .2f, Screen.width * .8f), Mathf.Clamp(transform.position.y, Screen.height * .2f, Screen.height * .8f), mainCam.nearClipPlane);
        }
    }
}
