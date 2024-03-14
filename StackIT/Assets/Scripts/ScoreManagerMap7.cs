using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreManagerMap7 : MonoBehaviour
{

    private static ScoreManagerMap7 instance;
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
    private bool isLockRemoved7 = false;
    private bool isButtonEnabled7 = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey7 = "LockState7";
    private const string ButtonStateKey7 = "ButtonState7";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage8StateKey = "LockedImage8State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage7ShownKey = "UnlockMessage7Shown";

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

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing7 = PlayerPrefs.GetInt(UnlockMessage7ShownKey, 0) == 1;
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        isLockRemoved7 = PlayerPrefs.GetInt(LockStateKey7, 0) == 1;
        isButtonEnabled7 = PlayerPrefs.GetInt(ButtonStateKey7, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage8Active = PlayerPrefs.GetInt(LockedImage8StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton8.interactable = isButtonEnabled7;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage8.SetActive(isLockedImage8Active);
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
        else if (playerScore == 3)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";
        }

        if (playerScore == 25)
        {
            // Remove lockedItemImage6 when the player score reaches 25
            lockedItemImage8.SetActive(false);
            PlayerPrefs.SetInt(LockedImage8StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing7)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText7.text = "";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage7ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();

            // Enable the button when the player score reaches 25 and the highest score is 25
            if (playerScore == 25 && highScoreManager.GetHighestScore() == 25)
            {
                isButtonEnabled7 = true;
                PlayerPrefs.SetInt(ButtonStateKey7, 1); // Save the button state
                LoadAndApplyButtonAndLockedImageStates(); // Load and apply the button and locked image states
            }
        }

        // Remove the if condition
        yourButton8.interactable = isButtonEnabled7;
        PlayerPrefs.Save();

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
        isUnlockMessageShowing = true;
        unlockText7.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText7.gameObject.SetActive(false);
        isUnlockMessageShowing7 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey7, 1);
        PlayerPrefs.Save();
        isLockRemoved7 = true;
    }
}