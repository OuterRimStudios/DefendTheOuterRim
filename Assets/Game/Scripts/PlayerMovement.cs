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

    bool increasingSpeed;
    bool decreasingSpeed;

    public RectTransform reticle;

    float speed;

    Rigidbody rb;

    private Quaternion qTo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = baseForwardSpeed;
    }

	public void Move (Vector2 cursorVector, Vector2 radialVector, bool thrust)
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;

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

        float newAngle = Vector3.Angle(new Vector3(Screen.width, 0, 0), new Vector3(cursorVector.x, cursorVector.y, 0));

        //print("Cursor Y Vector: " + screenSpace.y + " Screen Height / 2: " + Screen.height / 2);

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
