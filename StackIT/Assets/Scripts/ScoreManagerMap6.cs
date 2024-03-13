
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerMap6 : MonoBehaviour
{
    private static ScoreManagerMap6 instance;
    public ThreeXButtonMap6 threeXButtonScriptMap6;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText6; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage7; // Reference to the second locked image
    public Button yourButton7; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing6 = false; // Track the second unlock message
    private bool isLockRemoved6 = false;
    private bool isButtonEnabled6 = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey6 = "LockState6";
    private const string ButtonStateKey6 = "ButtonState6";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage7StateKey = "LockedImage7State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage6ShownKey = "UnlockMessage6Shown";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Awake()
    {
        // Make this instance persistent across scene changes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();

        // Load the saved states
        // isLockRemoved6 = PlayerPrefs.GetInt(LockStateKey6, 0) == 1;
        // isButtonEnabled6 = PlayerPrefs.GetInt(ButtonStateKey6, 0) == 1;
        // bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        // bool isLockedImage7Active = PlayerPrefs.GetInt(LockedImage7StateKey, 1) == 1;

        // // Set the button and locked images according to the saved states
        // yourButton7.interactable = isButtonEnabled6;
        // lockedItemImage.SetActive(isLockedImageActive);
        // lockedItemImage7.SetActive(isLockedImage7Active);
        // PlayerPrefs.Save();

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing6 = PlayerPrefs.GetInt(UnlockMessage6ShownKey, 0) == 1;
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        isLockRemoved6 = PlayerPrefs.GetInt(LockStateKey6, 0) == 1;
        isButtonEnabled6 = PlayerPrefs.GetInt(ButtonStateKey6, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage7Active = PlayerPrefs.GetInt(LockedImage7StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton7.interactable = isButtonEnabled6;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage7.SetActive(isLockedImage7Active);
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
        else if (playerScore == 3)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";
        }

        if (playerScore == 30)
        {
            // Remove lockedItemImage7 when the player score reaches 30
            lockedItemImage7.SetActive(false);
            PlayerPrefs.SetInt(LockedImage7StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing6)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText6.text = "7th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage6ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();

            // Enable the button when the player score reaches 30 and the highest score is 30
            if (playerScore == 30 && highScoreManager.GetHighestScore() == 30)
            {
                isButtonEnabled6 = true;
                PlayerPrefs.SetInt(ButtonStateKey6, 1); // Save the button state
                LoadAndApplyButtonAndLockedImageStates(); // Load and apply the button and locked image states
            }
        }

        // Remove the if condition
        yourButton7.interactable = isButtonEnabled6;
        PlayerPrefs.Save();

        if (threeXButtonScriptMap6.IsButtonActive())
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
        isUnlockMessageShowing6 = true;
        unlockText6.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText6.gameObject.SetActive(false);
        isUnlockMessageShowing6 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey6, 1);
        PlayerPrefs.Save();
        isLockRemoved6 = true;
    }
}