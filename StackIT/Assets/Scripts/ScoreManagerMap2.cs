using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManagerMap2 : MonoBehaviour
{

    private static ScoreManagerMap2 instance;

    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText2; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage3; // Reference to the second locked image
    public Button yourButton3; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing2 = false; // Track the second unlock message
    private bool isLockRemoved2 = false;
    private bool isButtonEnabled2 = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey2 = "LockState2";
    private const string ButtonStateKey2 = "ButtonState2";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage3StateKey = "LockedImage3State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage2ShownKey = "UnlockMessage2Shown";


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
        isUnlockMessageShowing2 = PlayerPrefs.GetInt(UnlockMessage2ShownKey, 0) == 1;
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        isLockRemoved2 = PlayerPrefs.GetInt(LockStateKey2, 0) == 1;
        isButtonEnabled2 = PlayerPrefs.GetInt(ButtonStateKey2, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage3Active = PlayerPrefs.GetInt(LockedImage3StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton3.interactable = isButtonEnabled2;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage3.SetActive(isLockedImage3Active);
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

        if (playerScore == 10 && !isUnlockMessageShowing)
        {
            // Remove lock images when the player score reaches 3
            RemoveLockImages();
            // Also remove lockedItemImage
            lockedItemImage.SetActive(false);
            PlayerPrefs.SetInt(LockStateKey2, 1);
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

        if (playerScore == 10)
        {
            // Remove lockedItemImage1 when the player score reaches 4
            lockedItemImage3.SetActive(false);
            PlayerPrefs.SetInt(LockedImage3StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed
            PlayerPrefs.Save();

            if (!isUnlockMessageShowing2)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText2.text = "";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage2ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();

            // Enable the button when the player score reaches 10 and the highest score is 10
            if (playerScore == 10 && highScoreManager.GetHighestScore() == 10)
            {
                isButtonEnabled2 = true;
                PlayerPrefs.SetInt(ButtonStateKey2, 1); // Save the button state
                LoadAndApplyButtonAndLockedImageStates(); // Load and apply the button and locked image states
            }
        }

        // Remove the if condition
        yourButton3.interactable = true;
        PlayerPrefs.Save();

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
        isUnlockMessageShowing2 = true;
        unlockText2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText2.gameObject.SetActive(false);
        isUnlockMessageShowing2 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey2, 1);
        isLockRemoved2 = true;
        PlayerPrefs.Save();
    }
}