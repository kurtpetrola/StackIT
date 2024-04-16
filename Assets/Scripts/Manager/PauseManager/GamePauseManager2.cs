using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager2 : MonoBehaviour
{
    public GameObject pausePanel; // Reference to the UI panel that should be displayed
    public ScoreManagerMap2 scoreManager; // Reference to the ScoreManagerMap1 script
    public int scoreToPauseAt = 10; // The score at which the game should pause and show the panel

    private void OnEnable()
    {
        // Subscribe to the ScoreChanged event
        scoreManager.ScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the ScoreChanged event
        scoreManager.ScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(int currentScore)
    {
        // Check if the score has reached the threshold
        if (currentScore >= scoreToPauseAt)
        {
            PauseGameAndShowPanel();
        }
    }

    private void PauseGameAndShowPanel()
    {
        // Pause the game
        Time.timeScale = 0;
        // Show the pause panel
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    // Call this method to resume the game from the UI button
    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1;
        // Hide the pause panel
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
}