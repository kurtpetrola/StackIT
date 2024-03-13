using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManagerMap3 : MonoBehaviour
{

    private static ScoreManagerMap3 instance;
    public TwoXButton twoXButtonScript;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText3; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage4; // Reference to the second locked image
    public Button yourButton4; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing3 = false; // Track the second unlock message
    private bool isLockRemoved3 = false;
    private bool isButtonEnabled3 = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey3 = "LockState3";
    private const string ButtonStateKey3 = "ButtonState3";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage4StateKey = "LockedImage4State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage3ShownKey = "UnlockMessage3Shown";

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

        // // Load the saved states
        // isLockRemoved3 = PlayerPrefs.GetInt(LockStateKey3, 0) == 1;
        // isButtonEnabled3 = PlayerPrefs.GetInt(ButtonStateKey3, 0) == 1;
        // bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        // bool isLockedImage4Active = PlayerPrefs.GetInt(LockedImage4StateKey, 1) == 1;

        // // Set the button and locked images according to the saved states
        // yourButton4.interactable = isButtonEnabled3;
        // lockedItemImage.SetActive(isLockedImageActive);
        // lockedItemImage4.SetActive(isLockedImage4Active);
        // PlayerPrefs.Save();

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing3 = PlayerPrefs.GetInt(UnlockMessage3ShownKey, 0) == 1;
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        isLockRemoved3 = PlayerPrefs.GetInt(LockStateKey3, 0) == 1;
        isButtonEnabled3 = PlayerPrefs.GetInt(ButtonStateKey3, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage4Active = PlayerPrefs.GetInt(LockedImage4StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton4.interactable = isButtonEnabled3;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage4.SetActive(isLockedImage4Active);
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
            PlayerPrefs.SetInt(LockStateKey3, 1);
            PlayerPrefs.SetInt(LockedImageStateKey, 0);
            PlayerPrefs.Save();

            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "2x Item Is Unlocked";

            // Mark the unlock message as shown
            PlayerPrefs.SetInt(UnlockMessageShownKey, 1);
            PlayerPrefs.Save();
        }
        else if (playerScore == 3)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";
        }

        if (playerScore == 15)
        {
            // Remove lockedItemImage4 when the player score reaches 15
            lockedItemImage4.SetActive(false);
            PlayerPrefs.SetInt(LockedImage4StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing3)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText3.text = "4th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage3ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();

            // Enable the button when the player score reaches 15 and the highest score is 15
            if (playerScore == 15 && highScoreManager.GetHighestScore() == 15)
            {
                isButtonEnabled3 = true;
                PlayerPrefs.SetInt(ButtonStateKey3, 1); // Save the button state
                LoadAndApplyButtonAndLockedImageStates(); // Load and apply the button and locked image states
            }
        }

        // Remove the if condition
        yourButton4.interactable = true;
        PlayerPrefs.Save();

        if (twoXButtonScript.IsButtonActive())
        {
            playerScore += 1;
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
        isUnlockMessageShowing3 = true;
        unlockText3.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText3.gameObject.SetActive(false);
        isUnlockMessageShowing3 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey3, 1);
        isLockRemoved3 = true;
        PlayerPrefs.Save();
    }
}