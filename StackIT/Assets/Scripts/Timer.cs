using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float elapsedTime = 0f;
    public bool isTimerRunning = false;
    public delegate void TimerTick(float time);
    public event TimerTick OnTimerTick;
    public delegate void TimerPause(bool isPaused);
    public event TimerPause OnTimerPause;


    public float GetTime()
    {
        return elapsedTime;
    }


    public void SetTime(float time)
    {
        elapsedTime = time;
    }


    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartTimer();
        }
    }


    private void Update()
    {

        if (isTimerRunning)
        {

            elapsedTime += Time.deltaTime;


            OnTimerTick?.Invoke(elapsedTime);


            //Debug.Log("Elapsed Time: " + FormatTime(elapsedTime));
        }
    }


    public void StartTimer()
    {
        isTimerRunning = true;
        OnTimerPause?.Invoke(false);
    }


    public void PauseTimer()
    {
        isTimerRunning = false;
        OnTimerPause?.Invoke(true);
    }


    public void ResetTimer()
    {
        isTimerRunning = false;
        elapsedTime = 0f;
    }

    private string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
