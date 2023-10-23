using UnityEngine;
using UnityEngine.UI;

public class ButtonLock : MonoBehaviour
{
    public Button buttonToLock;
    public ScoreManager scoreManager;

    private void Awake()
    {
        // Subscribe to the ScoreChanged event
        scoreManager.ScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        Debug.Log("Player Score: " + newScore);
        buttonToLock.interactable = IsButtonClickable(newScore);
    }

    private bool IsButtonClickable(int playerScore)
    {
        return playerScore <= 2;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the ScoreChanged event to prevent memory leaks
        scoreManager.ScoreChanged -= OnScoreChanged;
    }
}
