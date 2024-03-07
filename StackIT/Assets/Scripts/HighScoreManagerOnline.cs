using UnityEngine;
using UnityEngine.UI;

public class HighScoreManagerOnline : MonoBehaviour
{
    public Text highestScoreTextOnline;

    private string onlineHighestScoreKey = "OnlineHighestScore"; // New key for online mode

    void Start()
    {
        LoadHighestScore();
    }

    public void LoadHighestScore()
    {
        int highestScore = PlayerPrefs.GetInt(onlineHighestScoreKey, 0);
        highestScoreTextOnline.text = "Highest Score (Online): " + highestScore;
    }

    public void SetHighestScore(int score)
    {
        PlayerPrefs.SetInt(onlineHighestScoreKey, score); // Store online high score using the new key
        PlayerPrefs.Save();
        highestScoreTextOnline.text = "Highest Score (Online): " + score;
    }

    public int GetHighestScore()
    {
        return PlayerPrefs.GetInt(onlineHighestScoreKey, 0);
    }
}