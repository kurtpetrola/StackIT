using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoring : MonoBehaviour
{
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the locked image in the shop

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isLockRemoved = false;

    // PlayerPrefs keys
    private const string LockStateKey = "LockState";

   void Start()
{
    LoadHighestScore();
    UpdateHighestScoreUI();
    
    // Check if the lock has already been removed
    isLockRemoved = PlayerPrefs.GetInt(LockStateKey, 0) == 1;

   /* if (isLockRemoved)
    {
        unlockText.text = "2x Item Is Unlock";
    }
    else
    {
        unlockText.text = "2x item is Activate"; // Initial message when the lock is not removed
    }*/
}

   public void IncreaseScore()
{
    playerScore++;

    if (playerScore > highScoreManager.GetHighestScore())
    {
        highScoreManager.SetHighestScore(playerScore);
        UpdateHighestScoreUI();
    }

    if (playerScore == 3 && !isUnlockMessageShowing)
    {
        if (isLockRemoved)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "2X Item Activated";
        }
        else
        {
            RemoveLockImage();
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "2x Item Is Unlock";
        }
    }

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
        unlockText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }

    private void RemoveLockImage()
    {
        lockedItemImage.SetActive(false);
        PlayerPrefs.SetInt(LockStateKey, 1);
        PlayerPrefs.Save();
        isLockRemoved = true;
    }
}
