using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfter : MonoBehaviour
{
    public float turnOffAfter = 1f;

	void OnEnable ()
    {
        StartCoroutine(TurnObjectOffAfter());
	}

    IEnumerator TurnObjectOffAfter()
    {
        yield return new WaitForSeconds(turnOffAfter);
        gameObject.SetActive(false);
    }
}
