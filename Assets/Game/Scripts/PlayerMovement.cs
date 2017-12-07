using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float baseForwardSpeed;
    public float maxForwardSpeed;

    public float speedIncreaseAmount;
    public float speedDecreaseAmount;

    public float speedIncreaseFrequency;
    public float speedDecreaseFrequency;


    [Space, Header("Rotations")]

    public float outerZoneRotationSpeed;
    public float midZoneRotationSpeed;
	public float rotationSpeed;

    public float cursorOffset;
    public float clampRadius = 100;

    public Vector3 upAngle = new Vector3(-22.5f, 0,0);
    public Vector3 upRightAngle = new Vector3(-22.5f, 22.5f, -22.5f);
    public Vector3 upLeftAngle = new Vector3(-22.5f, -22.5f, 22.5f);

    public Vector3 downAngle = new Vector3(22.5f, 0, -0);
    public Vector3 downRightAngle = new Vector3(22.5f, 22.5f, -22.5f);
    public Vector3 downLeftAngle = new Vector3(22.5f, -22.5f, 22.5f);

    public Vector3 rightAngle = new Vector3(0, 22.5f, -22.5f);
    public Vector3 leftAngle = new Vector3(0, -22.5f, 22.5f);

    [Space]
    public float deadzone;

    bool increasingSpeed;
    bool decreasingSpeed;

    float speed;

    Vector3 initialPos;

    Rigidbody rb;

    private Quaternion qTo;

	bool playerOne;

	public float clampValue = 35f;
    Camera mainCam;
	GameObject cursor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        speed = baseForwardSpeed;

		if (GetComponent<PlayerInput> ().playerID == 0)
			playerOne = true;

		cursor = transform.Find ("Cursor").gameObject;
		cursor.transform.parent = null;

    }

    public void Move(Vector2 cursorVector, bool thrust, bool hasMouse)
    {
        if (!hasMouse)
        {
            float moveX = cursorVector.x * speed * Time.deltaTime;
            float moveY = cursorVector.y * speed * Time.deltaTime;
            cursor.transform.position += new Vector3(moveX, moveY, 0f);
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            float moveX = mainCam.ScreenToWorldPoint(mousePos).x;
            float moveY = mainCam.ScreenToWorldPoint(mousePos).y;
            mousePos.z = 10;
            cursor.transform.position = mainCam.ScreenToWorldPoint(mousePos);
        }

        //transform.LookAt (cursor.transform.position);
        Vector3 targetPos = new Vector3(cursor.transform.position.x, cursor.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);


        //		Vector3 targetRot = cursor.transform.position - transform.position;
        //		Quaternion rotation = Quaternion.LookRotation (targetRot);
        //
        //		transform.rotation = rotation;





        //if (!playerOne)
        //{
        //    //Vector3 newPos = transform.position + new Vector3(cursorVector.x, cursorVector.y, 0f);
        //    Vector3 playerOnePos = PlayerWrangler.GetPlayer(0).transform.position;
        //    Vector3 offset = new Vector3(5, 5, 0) + playerOnePos;
        //    transform.position = playerOnePos + Vector3.ClampMagnitude(offset, clampRadius);
        //}
        //else
        //rb.velocity = transform.forward * speed * Time.deltaTime;

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

        Rotate(cursorVector);
    }

    void Rotate(Vector2 cursorVector)
	{
		Vector3 screenSpace = mainCam.WorldToScreenPoint(cursor.transform.position);
		if (screenSpace.x > Screen.width / .4f || screenSpace.x < Screen.width / .6f || screenSpace.y > Screen.height / .4f || screenSpace.y < Screen.height / .6f) return;

		float newAngle = Vector3.Angle(mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) + new Vector3(0,mainCam.transform.position.y,0), new Vector3(cursor.transform.position.x, cursor.transform.position.y, 0));
        //

        if (screenSpace.y > Screen.height / 2f)
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
        else if (screenSpace.y < Screen.height / 2f)
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
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime);

        // if (newAngle > -cursorOffset && newAngle < cursorOffset && newAngle > -cursorOffset && newAngle < cursorOffset)
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime);

    }

    IEnumerator ChangeSpeed(float amount, float frequency)
    {
        speed += amount;
        yield return new WaitForSeconds(frequency);
        increasingSpeed = false;
        decreasingSpeed = false;
    }
}
