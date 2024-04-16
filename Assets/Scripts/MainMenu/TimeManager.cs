using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public Timer timer;


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
