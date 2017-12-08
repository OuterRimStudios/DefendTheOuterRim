using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float baseForwardSpeed = 500f;
    public float maxForwardSpeed = 1000f;
    public float horizontalSpeed = 10f;
    public float shipFollowSpeed = 7.5f;

    public float speedIncreaseAmount = 100f;
    public float speedDecreaseAmount = 200f;

    public float speedIncreaseFrequency = .5f;
    public float speedDecreaseFrequency = .5f;

    [Space, Header("Rotation Variables")]
    public float rotationSpeed = 100f;
    public float cursorClampX = 30f;
    public float cursorClampYMin = 15f;
    public float cursorClampYMax = 20f;
    public float xClamp = 25f;
    public float yClamp = 25f;
    public float zClamp = 25f;

    bool increasingSpeed;
    bool decreasingSpeed;

    float speed;

    Vector3 initialPos;

    Rigidbody rb;

    private Quaternion qTo;

	bool playerOne;
    Camera mainCam;
	GameObject cursor;

    float rotationX;
    float rotationY;
    float rotationZ;

    float moveX;
    float moveY;

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
        cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, transform.position.z);

        if (!hasMouse)
        {
            moveX = cursorVector.x * (horizontalSpeed * 5) * Time.deltaTime;
            moveY = cursorVector.y * (horizontalSpeed * 5) * Time.deltaTime;
            cursor.transform.position += new Vector3(moveX, moveY, 0f);

            ControllerRotate(cursorVector);
        }
        else
        {
            moveX = cursorVector.x * (horizontalSpeed) * Time.deltaTime;
            moveY = cursorVector.y * (horizontalSpeed) * Time.deltaTime;
            cursor.transform.position += new Vector3(moveX, moveY, 0f);

            MouseRotate(cursorVector);
        }

        cursor.transform.position = new Vector3(Mathf.Clamp(cursor.transform.position.x, -cursorClampX, cursorClampX),
            Mathf.Clamp(cursor.transform.position.y, -cursorClampYMin, cursorClampYMax), cursor.transform.position.z);

        //Vector3 cursorPos = new Vector3(moveX, moveY, 0);
        //Vector3 newPos = cursor.transform.position + cursorPos;
        //Vector3 offset = newPos - initialPos;
        //cursor.transform.position = initialPos + Vector3.ClampMagnitude(offset, cursorClampRadius);

        Vector3 targetPos = new Vector3(cursor.transform.position.x, cursor.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, shipFollowSpeed * Time.deltaTime);

        transform.localEulerAngles = new Vector3(-rotationX, rotationY, -rotationZ);

       // rb.velocity = Vector3.forward * speed * Time.deltaTime;

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

    void ControllerRotate(Vector2 cursorVector)
    {
        rotationX = Mathf.Lerp(rotationX, 0, .1f);
        rotationX += cursorVector.y * (rotationSpeed * 15) * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -xClamp, xClamp);

        rotationY = Mathf.Lerp(rotationY, 0, .1f);
        rotationY += cursorVector.x * (rotationSpeed * 15) * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -yClamp, yClamp);

        rotationZ = Mathf.Lerp(rotationZ, 0, .1f);
        rotationZ += cursorVector.x * (rotationSpeed * 15) * Time.deltaTime;
        rotationZ = Mathf.Clamp(rotationZ, -zClamp, zClamp);
    }

    void MouseRotate(Vector2 cursorVector)
    {
        if(cursorVector == Vector2.zero)
        {
            rotationX = Mathf.Lerp(rotationX, 0, .1f);
            rotationY = Mathf.Lerp(rotationY, 0, .1f);
            rotationZ = Mathf.Lerp(rotationZ, 0, .1f);
        }
        
        rotationX += cursorVector.y * rotationSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -xClamp, xClamp);
        
        rotationY += cursorVector.x * rotationSpeed * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -yClamp, yClamp);
        
        rotationZ += cursorVector.x * rotationSpeed * Time.deltaTime;
        rotationZ = Mathf.Clamp(rotationZ, -zClamp, zClamp);
    }

    IEnumerator ChangeSpeed(float amount, float frequency)
    {
        speed += amount;
        yield return new WaitForSeconds(frequency);
        increasingSpeed = false;
        decreasingSpeed = false;
    }
}
