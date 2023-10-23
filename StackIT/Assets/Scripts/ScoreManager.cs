using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage1; // Reference to the first locked image
    public GameObject lockedItemImage2; // Reference to the second locked image
    public Button yourButton; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isLockRemoved = false;
    private bool isButtonEnabled = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey = "LockState";
    private const string ButtonStateKey = "ButtonState";
    private const string LockedImage1StateKey = "LockedImage1State";
    private const string LockedImage2StateKey = "LockedImage2State";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();

        // Load the saved states
        isLockRemoved = PlayerPrefs.GetInt(LockStateKey, 0) == 1;
        isButtonEnabled = PlayerPrefs.GetInt(ButtonStateKey, 0) == 1;
        bool isLockedImage1Active = PlayerPrefs.GetInt(LockedImage1StateKey, 1) == 1;
        bool isLockedImage2Active = PlayerPrefs.GetInt(LockedImage2StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton.interactable = isButtonEnabled;
        lockedItemImage1.SetActive(isLockedImage1Active);
        lockedItemImage2.SetActive(isLockedImage2Active);
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void IncreaseScore()
    {
        playerScore++;

        if (playerScore > highScoreManager.GetHighestScore())
        {
            highScoreManager.SetHighestScore(playerScore);
            UpdateHighestScoreUI();
        }

        if (playerScore == 3)
        {
            RemoveLockImages();
            if (!isUnlockMessageShowing)
            {
                StartCoroutine(ShowUnlockMessage());
                unlockText.text = "2x Item Is Unlock";
            }
        }

        if (playerScore == 3 && highScoreManager.GetHighestScore() == 3)
        {
            isButtonEnabled = true;
            PlayerPrefs.SetInt(ButtonStateKey, 1); // Save the button state
        }

        if (isButtonEnabled)
        {
            yourButton.interactable = true;
        }

        if (playerScore == 4)
        {
            stackedItems++;
        }

        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);

        // Update the saved state of locked images
        PlayerPrefs.SetInt(LockedImage1StateKey, lockedItemImage1.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt(LockedImage2StateKey, lockedItemImage2.activeSelf ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadHighestScore()
    {
        highScoreManager.LoadHighestScore();
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highScoreManager.GetHighestScore().ToString();
    }

    private System.Collections.IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }

    private void RemoveLockImages()
    {
        lockedItemImage1.SetActive(false);
        lockedItemImage2.SetActive(false);
        PlayerPrefs.SetInt(LockStateKey, 1);
        PlayerPrefs.Save();
        isLockRemoved = true;
    }
}
