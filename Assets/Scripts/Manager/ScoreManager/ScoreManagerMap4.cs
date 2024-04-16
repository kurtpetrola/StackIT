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
    public Text unlockText;
    public Text unlockText4; // Reference to the second unlock text
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
        // Load and apply the button and locked image states
        LoadAndApplyButtonAndLockedImageStates();

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing4 = PlayerPrefs.GetInt(UnlockMessage4ShownKey, 0) == 1;

        // Update the button's interactable state based on the player score and highest score
        UpdateButtonInteractableState();
    }

    private void LoadAndApplyButtonAndLockedImageStates()
    {
        // Load the saved states
        isLockRemoved4 = PlayerPrefs.GetInt(LockStateKey4, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage5Active = PlayerPrefs.GetInt(LockedImage5StateKey, 1) == 1;

        // Set the locked images according to the current states
        lockedItemImage.SetActive(isLockedImageActive && !isLockRemoved4);
        lockedItemImage5.SetActive(isLockedImage5Active && !isLockRemoved4);

        // Set the button's interactable state
        yourButton5.interactable = isButtonEnabled4;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void IncreaseScore()
    {
        playerScore++;


        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            // Remove lock images when the player score reaches 3
            RemoveLockImages();
            lockedItemImage.SetActive(false);
            PlayerPrefs.SetInt(LockedImageStateKey, 0);
            PlayerPrefs.Save();

            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";

            // Mark the unlock message as shown
            PlayerPrefs.SetInt(UnlockMessageShownKey, 1);
            PlayerPrefs.Save();

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();
        }
        else if (playerScore == 3)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";
        }

        if (playerScore == 20)
        {
            // Remove lockedItemImage5 when the player score and highest score reach 20
            lockedItemImage5.SetActive(false);
            PlayerPrefs.SetInt(LockedImage5StateKey, 0);
            isLockRemoved4 = true;
            PlayerPrefs.SetInt(LockStateKey4, 1);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing4)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText4.text = "";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage4ShownKey, 1);
                PlayerPrefs.Save();
            }

            // Enable the button when the player score and highest score reach 20
            isButtonEnabled4 = true;
            PlayerPrefs.SetInt(ButtonStateKey4, 1);

            // Load and apply the button and locked image states
            LoadAndApplyButtonAndLockedImageStates();
        }

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
        isLockRemoved4 = true;
        PlayerPrefs.Save();
    }

    private void UpdateButtonInteractableState()
    {
        isButtonEnabled4 = playerScore == 20;
        yourButton5.interactable = isButtonEnabled4;
    }
}