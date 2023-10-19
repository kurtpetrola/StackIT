using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager

    private int playerScore = 0;
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

        if (playerScore > highScoreManager.GetHighestScore())
        {
            highScoreManager.SetHighestScore(playerScore);
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
        highScoreManager.LoadHighestScore();
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highScoreManager.GetHighestScore().ToString();
    }

    private IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.text = "2X Item Activated";
        unlockText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }
}