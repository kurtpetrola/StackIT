using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerMap7 : MonoBehaviour
{
    public ThreeXButtonMap7 threeXButtonScriptMap7;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText7; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage8; // Reference to the second locked image
    public Button yourButton8; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing7 = false; // Track the second unlock message
    private bool isLockRemoved = false;
    private bool isButtonEnabled = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey7 = "LockState7";
    private const string ButtonStateKey7 = "ButtonState7";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage8StateKey = "LockedImage8State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage7ShownKey = "UnlockMessage7Shown";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();

        // Load the saved states
        isLockRemoved = PlayerPrefs.GetInt(LockStateKey7, 0) == 1;
        isButtonEnabled = PlayerPrefs.GetInt(ButtonStateKey7, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage8Active = PlayerPrefs.GetInt(LockedImage8StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton8.interactable = isButtonEnabled;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage8.SetActive(isLockedImage8Active);

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing7 = PlayerPrefs.GetInt(UnlockMessage7ShownKey, 0) == 1;
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
            PlayerPrefs.SetInt(LockStateKey7, 1);
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
        if (playerScore == 35)
        {
            // Remove lockedItemImage1 when the player score reaches 4
            lockedItemImage8.SetActive(false);
            PlayerPrefs.SetInt(LockedImage8StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing7)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText7.text = "8th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage7ShownKey, 1);
                PlayerPrefs.Save();
            }
        }

        if (playerScore == 4 && highScoreManager.GetHighestScore() == 4)
        {
            isButtonEnabled = true;
            PlayerPrefs.SetInt(ButtonStateKey7, 1); // Save the button state
        }

        if (isButtonEnabled)
        {
            yourButton8.interactable = true;
        }


        if (threeXButtonScriptMap7.IsButtonActive())
        {
            playerScore += 2;
        }

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
        isUnlockMessageShowing7 = true;
        unlockText7.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText7.gameObject.SetActive(false);
        isUnlockMessageShowing7 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey7, 1);
        PlayerPrefs.Save();
        isLockRemoved = true;
    }
}