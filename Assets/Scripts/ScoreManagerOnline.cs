using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerOnline : MonoBehaviour
{
    public Text scoreText;
    public Text scoreText1;
    public Text highestScoreTextOnline;
    public HighScoreManagerOnline highScoreManagerOnline;

    private int playerScore = 0;
    private int stackedItems = 0;

    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScoreOnline();
        UpdateHighestScoreUIOnline();
    }

    public void IncreaseScoreOnline()
    {
        playerScore++;


        playerScore += stackedItems;

        if (playerScore > highScoreManagerOnline.GetHighestScore())
        {
            highScoreManagerOnline.SetHighestScore(playerScore); // Set the highest score for online mode
            UpdateHighestScoreUIOnline(); // Update the UI for online mode
        }

        scoreText.text = "Score: " + playerScore.ToString();
        scoreText1.text = "Score: " + playerScore.ToString();
        ScoreChanged?.Invoke(playerScore);
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    private void LoadHighestScoreOnline()
    {
        highScoreManagerOnline.LoadHighestScore(); // Load the highest score for online mode
    }

    private void UpdateHighestScoreUIOnline()
    {
        highestScoreTextOnline.text = "Highest Score (Online): " + highScoreManagerOnline.GetHighestScore().ToString(); // Update UI for online mode
    }
}