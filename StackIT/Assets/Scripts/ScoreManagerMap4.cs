using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreManagerMap4 : MonoBehaviour
{

    private static ScoreManagerMap4 instance;
    public TwoXButtonMap4 twoXButtonScriptMap4;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText4; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage5; // Reference to the second locked image
    public Button yourButton5; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing4 = false; // Track the second unlock message
    private bool isLockRemoved4 = false;
    private bool isButtonEnabled4 = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey4 = "LockState4";
    private const string ButtonStateKey4 = "ButtonState4";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage5StateKey = "LockedImage5State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage4ShownKey = "UnlockMessage4Shown";

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
        // isLockRemoved4 = PlayerPrefs.GetInt(LockStateKey4, 0) == 1;
        // isButtonEnabled4 = PlayerPrefs.GetInt(ButtonStateKey4, 0) == 1;
        // bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        // bool isLockedImage5Active = PlayerPrefs.GetInt(LockedImage5StateKey, 1) == 1;

        // // Set the button and locked images according to the saved states
        // yourButton5.interactable = isButtonEnabled4;
        // lockedItemImage.SetActive(isLockedImageActive);
        // lockedItemImage5.SetActive(isLockedImage5Active);
        // PlayerPrefs.Save();

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing4 = PlayerPrefs.GetInt(UnlockMessage4ShownKey, 0) == 1;
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        // Load the saved states
        isLockRemoved4 = PlayerPrefs.GetInt(LockStateKey4, 0) == 1;
        isButtonEnabled4 = PlayerPrefs.GetInt(ButtonStateKey4, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage5Active = PlayerPrefs.GetInt(LockedImage5StateKey, 1) == 1;

        // Set the button and locked images according to the current states
        yourButton5.interactable = isButtonEnabled4;
        lockedItemImage.SetActive(!isLockRemoved4);
        lockedItemImage5.SetActive(!isLockRemoved4);
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

        if (playerScore == 20)
        {
            // Remove lockedItemImage5 when the player score reaches 20
            lockedItemImage5.SetActive(false);
            isLockRemoved4 = true;

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing4)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText4.text = "5th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage4ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Enable the button when the player score reaches 20 and the highest score is 20
            if (playerScore == 20 && highScoreManager.GetHighestScore() == 20)
            {
                isButtonEnabled4 = true;
            }

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();
        }

        // Remove the if condition
        yourButton5.interactable = isButtonEnabled4;
        PlayerPrefs.Save();

        if (twoXButtonScriptMap4.IsButtonActive())
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
        isUnlockMessageShowing4 = true;
        unlockText4.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText4.gameObject.SetActive(false);
        isUnlockMessageShowing4 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey4, 1);
        PlayerPrefs.Save();
        isLockRemoved4 = true;
    }
}