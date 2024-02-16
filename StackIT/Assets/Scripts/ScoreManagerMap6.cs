
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerMap6 : MonoBehaviour
{
    public ThreeXButtonMap5 threeXButtonScriptMap5;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText1; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage1; // Reference to the second locked image
    public Button yourButton6; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing1 = false; // Track the second unlock message
    private bool isLockRemoved = false;
    private bool isButtonEnabled = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey6 = "LockState6";
    private const string ButtonStateKey6 = "ButtonState6";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage1StateKey = "LockedImage1State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage1ShownKey = "UnlockMessage1Shown";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();

        // Load the saved states
        isLockRemoved = PlayerPrefs.GetInt(LockStateKey6, 0) == 1;
        isButtonEnabled = PlayerPrefs.GetInt(ButtonStateKey6, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage1Active = PlayerPrefs.GetInt(LockedImage1StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton6.interactable = isButtonEnabled;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage1.SetActive(isLockedImage1Active);

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing1 = PlayerPrefs.GetInt(UnlockMessage1ShownKey, 0) == 1;
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
            PlayerPrefs.SetInt(LockStateKey6, 1);
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
        if (playerScore == 4)
        {
            // Remove lockedItemImage1 when the player score reaches 4
            lockedItemImage1.SetActive(false);
            PlayerPrefs.SetInt(LockedImage1StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing1)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText1.text = "2ND Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage1ShownKey, 1);
                PlayerPrefs.Save();
            }
        }

        if (playerScore == 4 && highScoreManager.GetHighestScore() == 4)
        {
            isButtonEnabled = true;
            PlayerPrefs.SetInt(ButtonStateKey6, 1); // Save the button state
        }

        if (isButtonEnabled)
        {
            yourButton6.interactable = true;
        }


        if (threeXButtonScriptMap5.IsButtonActive())
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
        isUnlockMessageShowing1 = true;
        unlockText1.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText1.gameObject.SetActive(false);
        isUnlockMessageShowing1 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey6, 1);
        PlayerPrefs.Save();
        isLockRemoved = true;
    }
}