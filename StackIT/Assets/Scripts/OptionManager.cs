using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private bool isPaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;

        // Add logic that depends on 'isPaused' here.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;

        // Add logic that depends on 'isPaused' here.
    }
}
