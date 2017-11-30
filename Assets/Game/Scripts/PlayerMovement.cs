using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float baseForwardSpeed;
    public float maxForwardSpeed;

    public float speedIncreaseAmount;
    public float speedDecreaseAmount;

    public float speedIncreaseFrequency;
    public float speedDecreaseFrequency;

    bool increasingSpeed;
    bool decreasingSpeed;

    float speed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = baseForwardSpeed;
    }

	public void Move (Vector2 radialVector, bool thrust)
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;

        if (thrust && !increasingSpeed && speed < maxForwardSpeed)
        {
            increasingSpeed = true;
            StartCoroutine(ChangeSpeed(speedIncreaseAmount, speedIncreaseFrequency));
        }
        else if(!thrust && !decreasingSpeed && speed > baseForwardSpeed)
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
