using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerMap5 : MonoBehaviour
{

    private static ScoreManagerMap5 instance;
    public ThreeXButtonMap5 threeXButtonScriptMap5;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText5; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage6; // Reference to the second locked image
    public Button yourButton6; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing5 = false; // Track the second unlock message
    private bool isLockRemoved5 = false;
    private bool isButtonEnabled5 = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey5 = "LockState5";
    private const string ButtonStateKey5 = "ButtonState5";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage6StateKey = "LockedImage6State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage5ShownKey = "UnlockMessage5Shown";

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
        // isLockRemoved5 = PlayerPrefs.GetInt(LockStateKey5, 0) == 1;
        // isButtonEnabled5 = PlayerPrefs.GetInt(ButtonStateKey5, 0) == 1;
        // bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        // bool isLockedImage6Active = PlayerPrefs.GetInt(LockedImage6StateKey, 1) == 1;

        // // Set the button and locked images according to the saved states
        // yourButton6.interactable = isButtonEnabled5;
        // lockedItemImage.SetActive(isLockedImageActive);
        // lockedItemImage6.SetActive(isLockedImage6Active);
        // PlayerPrefs.Save();

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing5 = PlayerPrefs.GetInt(UnlockMessage5ShownKey, 0) == 1;
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        isLockRemoved5 = PlayerPrefs.GetInt(LockStateKey5, 0) == 1;
        isButtonEnabled5 = PlayerPrefs.GetInt(ButtonStateKey5, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage6Active = PlayerPrefs.GetInt(LockedImage6StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton6.interactable = isButtonEnabled5;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage6.SetActive(isLockedImage6Active);
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
            PlayerPrefs.SetInt(LockStateKey5, 1);
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

        if (playerScore == 25)
        {
            // Remove lockedItemImage6 when the player score reaches 25
            lockedItemImage6.SetActive(false);
            PlayerPrefs.SetInt(LockedImage6StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing5)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText5.text = "6th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage5ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();

            // Enable the button when the player score reaches 25 and the highest score is 25
            if (playerScore == 25 && highScoreManager.GetHighestScore() == 25)
            {
                isButtonEnabled5 = true;
                PlayerPrefs.SetInt(ButtonStateKey5, 1); // Save the button state
                LoadAndApplyButtonAndLockedImageStates(); // Load and apply the button and locked image states
            }
        }

        // Remove the if condition
        yourButton6.interactable = isButtonEnabled5;
        PlayerPrefs.Save();

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
        isUnlockMessageShowing5 = true;
        unlockText5.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText5.gameObject.SetActive(false);
        isUnlockMessageShowing5 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey5, 1);
        PlayerPrefs.Save();
        isLockRemoved5 = true;
    }
}