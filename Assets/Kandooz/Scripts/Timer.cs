using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;
using System.Threading;

public enum TimerType
{
    Increment,
    Decrement
}
public class Timer : MonoBehaviour {
    public Text text;
    public Text UITimer;
    public UnityEvent onTimeEnd;
    private TimerType timerTimerType;
    DateTime startTime;
    public GameObject exerciseManager;

    public bool TimerWorking = false;

    public int gameSeconds = 0;

    public int totalTime = 0;

    public int mins = 0;
    public int seconds = 0;
    public int timeScoreCounter = 0;

    private int step = 1;
    public static Timer Instance;

    public TimerType TimerTimerType
    {
        get
        {
            return timerTimerType;
        }

        set
        {
            timerTimerType = value;
            switch (value)
            {
                case TimerType.Increment:
                    step = 1;
                    break;
                case TimerType.Decrement:
                    step = -1; 
                    break;
            }
        }
    }

    private void Awake()
    {
        
        //timeScoreCounter = 
        if (Instance == null) Instance = this;
        TimerTimerType = TimerType.Decrement;
     
    }

    public void StopTimer()
    {
        TimeEnded();
        onTimeEnd.Invoke();
    }

    public void StartTimer(int totalSeconds,TimerType type = TimerType.Decrement)
    {
        TimerWorking = true;
        startTime = DateTime.Now;
        gameSeconds = totalSeconds/*+3*/;
        TimerTimerType = type;

        CalculateTime();
        StartCoroutine(TimeTick());
        totalTime = totalSeconds - 3;
        timeScoreCounter = 0;

    }

    

    private IEnumerator TimeTick()
    {
        while (true)
        {
            gameSeconds += step;

            if (TimerTimerType == TimerType.Decrement)
            {
                if (timeScoreCounter < totalTime)
                {
                    timeScoreCounter += 1;
                }
            }
            CalculateTime();

            if (TimerTimerType == TimerType.Decrement)
            {
                if (gameSeconds == 0) TimeEnded();
            }
               
            yield return new WaitForSeconds(1);

        }
    }

    private void CalculateTime()
    {
        mins = gameSeconds / 60;
        seconds = gameSeconds - mins * 60;
        UpdateText(seconds, mins);
    }

    public void TimeEnded()
    {
        TimerWorking = false;

        StopAllCoroutines();
        CalculateTime();
    }

    private void UpdateText(int seconds, int mins)
    {
        string minsString = mins.ToString();
        string secsString = seconds.ToString();

        if (mins < 10) minsString = "0" + mins.ToString();
        if (seconds < 10) secsString = "0" + seconds.ToString();

        text.text = (minsString + ":" + secsString);
        UITimer.text = text.text; 
    }

    private void TimerNotWorking()
    {
        text.text = " ";
        UITimer.text = text.text;

    }


    private void Update()
    {
        if (!TimerWorking)
        {
            TimerNotWorking();
        }
    }


    public void StartPauseTimer()
    {
        StartTimer(30, TimerType.Decrement);

    }
}
