using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerNormal : MonoBehaviour
{
    public Text scoreText;
    public Text highestScoreText;
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager

    private int playerScore = 0;
    private int stackedItems = 0;

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void IncreaseScore()
    {
        playerScore++;
        UpdateScoreUI();

        if (playerScore == 4)
        {
            // Increment the stacked items when the player score reaches 4
            stackedItems++;
        }

        // Add stacked items to the player's score
        playerScore += stackedItems;

        if (playerScore > highScoreManager.GetHighestScore())
        {
            highScoreManager.SetHighestScore(playerScore);
            UpdateHighestScoreUI();
        }

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
    }

    private void LoadHighestScore()
    {
        highScoreManager.LoadHighestScore();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highScoreManager.GetHighestScore().ToString();
    }
}
