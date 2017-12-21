using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public bool isTargeted;
    public Transform target;

	void Update ()
    {
        if(!isTargeted)
            transform.Translate(transform.forward * speed * Time.deltaTime);
        else
        {
            float smoothTime = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.transform.position, smoothTime);
        }
	}

    void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
