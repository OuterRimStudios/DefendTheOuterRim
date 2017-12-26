using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float baseForwardSpeed = 500f;
    public float maxForwardSpeed = 1000f;

    public float speedIncreaseAmount = 100f;
    public float speedDecreaseAmount = 200f;

    public float speedIncreaseFrequency = .5f;
    public float speedDecreaseFrequency = .5f;

    [Header("Cursor Variables")]
    public float cursorSensitiviy = 2.5f;
    public float controllerCursorSensitiviy = 50f;

    public float cursorClampXMin = -.5f;
    public float cursorClampXMax = 1.5f;
    public float cursorClampYMin = -.5f;
    public float cursorClampYMax = 1.5f;

    [Space, Header("Ship Variables")]
    public float shipFollowSpeed = 7.5f;
    public float shipRotationSpeed = 100f;

    public float shipClampXMin = -10f;
    public float shipClampXMax = 10f;
    public float shipClampYMin = -4f;
    public float shipClampYMax = 4f;

    [HideInInspector]
    public int playerID;

    float speed;
    float moveX;
    float moveY;

    Vector3 initialPos;

    Camera mainCam;
    GameObject cursor;
    GameObject reticle;

    bool playerOne;
    bool increasingSpeed;
    bool decreasingSpeed;

    void Awake()
    {
        mainCam = Camera.main;
        speed = baseForwardSpeed;

		if (GetComponent<PlayerInput> ().playerID == 0)
			playerOne = true;

        reticle = transform.Find("Reticle").gameObject;

        cursor = transform.Find ("Cursor").gameObject;
		cursor.transform.parent = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Move(Vector2 cursorVector, bool thrust, bool hasMouse)
    {
        if (!hasMouse)                                                                      //Checks if you are using controller or mouse
        {                                                                                   //and adjust sensitivty accordingly
            moveX = cursorVector.x * controllerCursorSensitiviy * Time.deltaTime;
            moveY = cursorVector.y * controllerCursorSensitiviy * Time.deltaTime;
            cursor.transform.localPosition += new Vector3(moveX, moveY, 0f);
        }
        else
        {
            moveX = cursorVector.x * cursorSensitiviy * Time.deltaTime;
            moveY = cursorVector.y * cursorSensitiviy * Time.deltaTime;
            cursor.transform.localPosition += new Vector3(moveX, moveY, 0f);
        }

        Vector3 cursorPos = mainCam.WorldToViewportPoint(cursor.transform.position);        //Find the cursor's position in the viewport

        cursorPos.x = Mathf.Clamp(cursorPos.x, cursorClampXMin, cursorClampXMax);           //Clamp the cursor's position within the viewport
        cursorPos.y = Mathf.Clamp(cursorPos.y, cursorClampYMin, cursorClampYMax);

        cursor.transform.position = mainCam.ViewportToWorldPoint(cursorPos);                //Set the cursor's position to the new clamped positon

        Vector3 targetPos = new Vector3(cursor.transform.localPosition.x, cursor.transform.localPosition.y, 5);             //Find the cursors position and set the ships position to it
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, shipFollowSpeed * Time.deltaTime);

        //Clamp the ships position within the viewport
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, shipClampXMin, shipClampXMax), Mathf.Clamp(transform.localPosition.y, shipClampYMin, shipClampYMax), transform.localPosition.z);

        Vector3 relativePos = reticle.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);                                                         //Make the ship face the cursor
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, shipRotationSpeed * Time.deltaTime);

        if (thrust && !increasingSpeed && speed < maxForwardSpeed)
        {
            increasingSpeed = true;
            StartCoroutine(ChangeSpeed(speedIncreaseAmount, speedIncreaseFrequency));
        }
        else if (!thrust && !decreasingSpeed && speed > baseForwardSpeed)
        {
            decreasingSpeed = true;
            StartCoroutine(ChangeSpeed(-speedDecreaseAmount, speedDecreaseFrequency));
        }
    }

    IEnumerator ChangeSpeed(float amount, float frequency)
    {
        speed += amount;
        yield return new WaitForSeconds(frequency);
        increasingSpeed = false;
        decreasingSpeed = false;
    }
}
