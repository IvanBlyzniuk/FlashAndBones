using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public string time;
    private float startTime;
    private bool isTimerRunning = false;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float floatTime = Time.time - startTime;
            time = FormatTime(floatTime);
            timerText.text = time;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}