using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    private float originalTimeScale;

    private void Start()
    {
        originalTimeScale = Time.timeScale;
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = originalTimeScale;
    }
}