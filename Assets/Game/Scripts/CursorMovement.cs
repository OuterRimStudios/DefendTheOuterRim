using UnityEngine;

public class CursorMovement : MonoBehaviour {

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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, Screen.width), Mathf.Clamp(transform.position.y, 0, Screen.height), mainCam.nearClipPlane);
        }
        else
        {
            transform.position = Input.mousePosition;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, Screen.width), Mathf.Clamp(transform.position.y, 0, Screen.height), mainCam.nearClipPlane);
        }
    }
}
