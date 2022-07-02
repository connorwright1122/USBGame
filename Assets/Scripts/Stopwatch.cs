using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = true;
    float currentTime;
    public Text currentTimeText;
    
    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if (stopwatchActive) 
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        //currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        currentTimeText.text = time.ToString(@"mm\:ss");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }
    
    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
}
