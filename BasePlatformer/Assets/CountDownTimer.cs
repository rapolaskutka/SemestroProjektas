using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startTime = 15f;

    public Text CountDownText;
    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            CountDownText.text = currentTime.ToString("00");
            CountDownText.color = Color.red;
        }
        else Application.Quit();
    }
}