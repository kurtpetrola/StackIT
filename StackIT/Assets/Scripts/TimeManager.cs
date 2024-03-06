using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;


    public Timer timer;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StartTimer()
    {
        if (timer != null)
        {
            timer.StartTimer();
        }
    }


    public void StopTimer()
    {
        if (timer != null)
        {
            timer.PauseTimer();
        }
    }


    public void ResetTimer()
    {
        if (timer != null)
        {
            timer.ResetTimer();
        }
    }
}
