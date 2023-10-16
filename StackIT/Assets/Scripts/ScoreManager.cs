using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;

    private int playerScore = 0;
    private int highestScore = 1;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;

    void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();
    }

    public void IncreaseScore()
    {
        playerScore++;

        if (playerScore > highestScore)
        {
            highestScore = playerScore;
            SaveHighestScore();
            UpdateHighestScoreUI();
        }

        // Check if an item is stacked
        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            StartCoroutine(ShowUnlockMessage());
        }

        // Check if an item is stacked
        if (playerScore == 4)
        {
            stackedItems++;
        }

        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();
    }

    private void LoadHighestScore()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
    }

    private void SaveHighestScore()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.Save();
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highestScore.ToString();
    }

    private IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.text = "2X Activated";
        unlockText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }
}
