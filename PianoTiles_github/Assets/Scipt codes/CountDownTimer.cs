using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // Decrement by the time passed in seconds since last frame
            countdownText.text = Mathf.Ceil(currentTime).ToString(); // Display the countdown on the UI
        }
        else
        {
            countdownText.text = "Time's Up!";
        }
    }
}
