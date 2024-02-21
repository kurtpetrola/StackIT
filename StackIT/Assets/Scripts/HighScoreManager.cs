using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text highestScoreText;

    void Start()
    {
        LoadHighestScore();
    }
    public void LoadHighestScore()
    {
        // int currentScore = 0; // This will be the score for the current game.
        int highestScore = PlayerPrefs.GetInt("HighestScore", 0);


        highestScoreText.text = "Highest Score: " + highestScore;

        // Use currentScore for your game logic.
        // When the game is over and you want to reset the high score to 1, call SetHighestScore(1).
    }

    public void SetHighestScore(int score)
    {
        PlayerPrefs.SetInt("HighestScore", score);
        PlayerPrefs.Save();
        highestScoreText.text = "Highest Score: " + score;
    }


    public int GetHighestScore()
    {
        return PlayerPrefs.GetInt("HighestScore", 0);
    }
}