using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public float speed;
    Camera mainCam;
    public float clampRadius = 500f;
    Vector3 initialPos;

    void Awake()
    {
        mainCam = Camera.main;
        initialPos = transform.position;
    }

    public void MoveCursor(float movex, float movey, bool hasMouse)
    {
        if (!hasMouse)
        {
            float moveX = movex * speed * Time.deltaTime;
            float moveY = movey * speed * Time.deltaTime;
            //transform.position += new Vector3(moveX, moveY, 0f);
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, Screen.width), Mathf.Clamp(transform.position.y, 0, Screen.height), mainCam.nearClipPlane);

            Vector3 cursorPos = new Vector3(moveX, moveY, 0);
            Vector3 newPos = transform.position + cursorPos;
            Vector3 offset = newPos - initialPos;
            transform.position = initialPos + Vector3.ClampMagnitude(offset, clampRadius);
        }
        else
        {
            transform.position = Input.mousePosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, Screen.width), Mathf.Clamp(transform.position.y, 0, Screen.height ), mainCam.nearClipPlane);
        }
    }
}
