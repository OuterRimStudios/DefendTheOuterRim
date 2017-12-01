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

    public RectTransform reticle;

    float speed;

    Rigidbody rb;

    private Quaternion qTo;

	bool playerOne;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = baseForwardSpeed;

		if (GetComponent<PlayerInput> ().playerID == 0)
			playerOne = true;
    }

	public void Move (Vector2 cursorVector, Vector2 radialVector, bool thrust)
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;

	//	transform.position = new Vector3(Mathf.Clamp(transform.position.x, Camera.main.transform.position.x + (-10), Camera.main.transform.position.x + 10)
	//		, Mathf.Clamp(transform.position.y,Camera.main.transform.position.y + (-8),Camera.main.transform.position.y + 8), transform.position.z);


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
        cursorVector = reticle.anchoredPosition;
        Vector3 screenSpace = Camera.main.ViewportToScreenPoint(cursorVector);
       // if (screenSpace.x > Screen.width / .4f || screenSpace.x < Screen.width / .6f || screenSpace.y > Screen.height / .4f || screenSpace.y < Screen.height / .6f) return;

        float newAngle = Vector3.Angle(new Vector3(Screen.width, 0, 0), new Vector3(cursorVector.x, cursorVector.y, 0));

        //print("Cursor Y Vector: " + screenSpace.y + " Screen Height / 2: " + Screen.height / 2);

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
