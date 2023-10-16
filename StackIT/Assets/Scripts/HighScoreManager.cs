using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public Text highestScoreText;

    void Start()
    {
        LoadHighestScore();
    }

    void LoadHighestScore()
    {
       
        int highestScore = PlayerPrefs.GetInt("HighestScore", 0);
         
        highestScoreText.text = "Highest Score: " + highestScore;

        // Increment highestScore by 1 to match the player's score
       

        // Save the updated highestScore
        SaveHighestScore(highestScore);
    }

    void SaveHighestScore(int score)
    {
        PlayerPrefs.SetInt("HighestScore", score);
        PlayerPrefs.Save();
    }
}
