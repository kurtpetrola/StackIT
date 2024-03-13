using UnityEngine;
using UnityEngine.UI;

public class ResetHighScore : MonoBehaviour
{
    public Button resetButton; // Reference to the reset button
    public Text highestScoreText; // Reference to the text displaying the highest score

    // Reference to the item locks
    public GameObject lockedItem1;
    public GameObject lockedItem2;
    public GameObject lockedItem3;

    // Reference to the map locks
    public GameObject lockedItemImage1;
    public GameObject lockedItemImage2;
    public GameObject lockedItemImage3;
    public GameObject lockedItemImage4;
    public GameObject lockedItemImage5;
    public GameObject lockedItemImage6;
    public GameObject lockedItemImage7;
    public GameObject lockedItemImage8;
    public GameObject lockedItemImage9;

    // Reference to the map buttons
    public Button yourButton2;
    public Button yourButton3;
    public Button yourButton4;
    public Button yourButton5;
    public Button yourButton6;
    public Button yourButton7;
    public Button yourButton8;
    public Button yourButton9;

    void Start()
    {
        // Attach a click event listener to the reset button
        resetButton.onClick.AddListener(ResetHighScoreButtonClick);
    }

    void ResetHighScoreButtonClick()
    {
        // Reset the high score to 0
        PlayerPrefs.SetInt("HighestScore", 0);

        // Set the items locked images to active
        PlayerPrefs.SetInt("LockedItem1State", 1);
        PlayerPrefs.SetInt("LockedItem2State", 1);
        PlayerPrefs.SetInt("LockedItem3State", 1);
        PlayerPrefs.Save();

        // Reset the Lock state to locked
        PlayerPrefs.SetInt("LockState", 0);
        PlayerPrefs.SetInt("LockState2", 0);
        PlayerPrefs.SetInt("LockState3", 0);
        PlayerPrefs.SetInt("LockState4", 0);
        PlayerPrefs.SetInt("LockState5", 0);
        PlayerPrefs.SetInt("LockState6", 0);
        PlayerPrefs.SetInt("LockState7", 0);
        PlayerPrefs.SetInt("LockState8", 0);
        PlayerPrefs.SetInt("LockState9", 0);
        PlayerPrefs.Save();

        // Reset the buttons state
        PlayerPrefs.SetInt("ButtonState", 0);
        PlayerPrefs.SetInt("ButtonState2", 0);
        PlayerPrefs.SetInt("ButtonState3", 0);
        PlayerPrefs.SetInt("ButtonState4", 0);
        PlayerPrefs.SetInt("ButtonState5", 0);
        PlayerPrefs.SetInt("ButtonState6", 0);
        PlayerPrefs.SetInt("ButtonState7", 0);
        PlayerPrefs.SetInt("ButtonState8", 0);
        PlayerPrefs.SetInt("ButtonState9", 0);
        PlayerPrefs.Save();

        // Set the locked images to active
        PlayerPrefs.SetInt("LockedImage1State", 1);
        PlayerPrefs.SetInt("LockedImage2State", 1);
        PlayerPrefs.SetInt("LockedImage3State", 1);
        PlayerPrefs.SetInt("LockedImage4State", 1);
        PlayerPrefs.SetInt("LockedImage5State", 1);
        PlayerPrefs.SetInt("LockedImage6State", 1);
        PlayerPrefs.SetInt("LockedImage7State", 1);
        PlayerPrefs.SetInt("LockedImage8State", 1);
        PlayerPrefs.SetInt("LockedImage9State", 1);
        PlayerPrefs.Save();

        // Reset the unlock message states
        PlayerPrefs.SetInt("UnlockMessageShown", 0);
        PlayerPrefs.SetInt("UnlockMessage1Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage2Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage3Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage4Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage5Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage6Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage7Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage8Shown", 0);
        PlayerPrefs.SetInt("UnlockMessage9Shown", 0);
        PlayerPrefs.Save();

        // Update the UI text to reflect the reset
        highestScoreText.text = "Highest Score: 0";

        // Show the locked item images after resetting
        lockedItem1.SetActive(true);
        lockedItem2.SetActive(true);
        lockedItem3.SetActive(true);
        PlayerPrefs.Save();

        // Show the locked images after resetting
        lockedItemImage1.SetActive(true);
        lockedItemImage2.SetActive(true);
        lockedItemImage3.SetActive(true);
        lockedItemImage4.SetActive(true);
        lockedItemImage5.SetActive(true);
        lockedItemImage6.SetActive(true);
        lockedItemImage7.SetActive(true);
        lockedItemImage8.SetActive(true);
        lockedItemImage9.SetActive(true);
        // lockedItemImage10.SetActive(true);
        PlayerPrefs.Save();

        // Set the button as not clickable
        yourButton2.interactable = false;
        yourButton3.interactable = false;
        yourButton4.interactable = false;
        yourButton5.interactable = false;
        yourButton6.interactable = false;
        yourButton7.interactable = false;
        yourButton8.interactable = false;
        yourButton9.interactable = false;
        PlayerPrefs.Save();
    }
}