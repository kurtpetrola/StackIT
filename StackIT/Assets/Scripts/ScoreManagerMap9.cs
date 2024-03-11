using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerMap9 : MonoBehaviour
{
    public FourXButtonMap9 fourXButtonScriptMap9;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText9; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage10; // Reference to the second locked image
    public Button yourButton10; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing9 = false; // Track the second unlock message
    private bool isLockRemoved = false;
    private bool isButtonEnabled = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey9 = "LockState9";
    private const string ButtonStateKey9 = "ButtonState9";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage10StateKey = "LockedImage10State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage9ShownKey = "UnlockMessage9Shown";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();

        // Load the saved states
        isLockRemoved = PlayerPrefs.GetInt(LockStateKey9, 0) == 1;
        isButtonEnabled = PlayerPrefs.GetInt(ButtonStateKey9, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage10Active = PlayerPrefs.GetInt(LockedImage10StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton10.interactable = isButtonEnabled;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage10.SetActive(isLockedImage10Active);

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing9 = PlayerPrefs.GetInt(UnlockMessage9ShownKey, 0) == 1;
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

        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            // Remove lock images when the player score reaches 3
            RemoveLockImages();
            // Also remove lockedItemImage
            lockedItemImage.SetActive(false);
            PlayerPrefs.SetInt(LockStateKey9, 1);
            PlayerPrefs.SetInt(LockedImageStateKey, 0);
            PlayerPrefs.Save();

            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";



            // Mark the unlock message as shown
            PlayerPrefs.SetInt(UnlockMessageShownKey, 1);
            PlayerPrefs.Save();

        }
        else if
                  (playerScore == 3)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";
        }
        if (playerScore == 45)
        {
            // Remove lockedItemImage1 when the player score reaches 4
            lockedItemImage10.SetActive(false);
            PlayerPrefs.SetInt(LockedImage10StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing9)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText9.text = "9th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage9ShownKey, 1);
                PlayerPrefs.Save();
            }
        }

        if (playerScore == 4 && highScoreManager.GetHighestScore() == 4)
        {
            isButtonEnabled = true;
            PlayerPrefs.SetInt(ButtonStateKey9, 1); // Save the button state
        }

        if (isButtonEnabled)
        {
            yourButton10.interactable = true;
        }


        if (fourXButtonScriptMap9.IsButtonActive())
        {
            playerScore += 3;
        }

        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
    }
    public void DecreaseScore()
    {
        playerScore -= 1;
        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
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

    private System.Collections.IEnumerator ShowUnlockMessage1()
    {
        isUnlockMessageShowing9 = true;
        unlockText9.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText9.gameObject.SetActive(false);
        isUnlockMessageShowing9 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey9, 1);
        PlayerPrefs.Save();
        isLockRemoved = true;
    }
}