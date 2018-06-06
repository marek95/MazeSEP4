using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject slider;
    public const float waitTime = 8f;
    float timeLeft;

    void Start()
    {
        timeLeft = waitTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        slider.GetComponent<Slider>().value = timeLeft/waitTime;

        if (timeLeft <= 0)
        {
            slider.SetActive(false);
        }
    }
}
