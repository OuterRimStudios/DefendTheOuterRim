using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinkEffect : MonoBehaviour
{
    public float blinkFrequency;

    float frequency;
    Text myImage;
    bool blinking;

    void Start()
    {
        myImage = GetComponent<Text>();
        frequency = blinkFrequency * .5f;
    }

    void Update()
    {
        if(!blinking)
        {
            blinking = true;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        myImage.enabled = false;
        yield return new WaitForSeconds(frequency);
        myImage.enabled = true;
        yield return new WaitForSeconds(frequency);
        blinking = false;
    }
}
