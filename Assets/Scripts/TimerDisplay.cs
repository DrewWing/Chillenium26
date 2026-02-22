using UnityEngine;
using TMPro;
using System.Collections;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 10f;
    public bool timerIsRunning = false;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("0:{0:00}", seconds);
    }
}
