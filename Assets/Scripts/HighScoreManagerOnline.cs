using UnityEngine;
using UnityEngine.UI;

public class HighScoreManagerOnline : MonoBehaviour
{
    public Text highestScoreTextOnline;
    private string onlineHighestScoreKey = "OnlineHighestScore";

    void Start()
    {
        LoadHighestScore();
    }

    public void LoadHighestScore()
    {
        int highestScore = PlayerPrefs.GetInt(onlineHighestScoreKey, 0);
        highestScoreTextOnline.text = "Highest Score: " + highestScore;
    }

    public void SetHighestScore(int score)
    {
        SubmitLeaderboardScore.Submit(score); // Submit the high score to the leaderboard
        PlayerPrefs.SetInt(onlineHighestScoreKey, score);
        PlayerPrefs.Save();
        highestScoreTextOnline.text = "Highest Score: " + score;
    }

    public int GetHighestScore()
    {
        return PlayerPrefs.GetInt(onlineHighestScoreKey, 0);
    }
}