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
        int highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        //highestScore++;
        highestScoreText.text = "Highest Score: " + highestScore;
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
