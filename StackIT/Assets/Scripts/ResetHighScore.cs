using UnityEngine;
using UnityEngine.UI;

public class ResetHighScore : MonoBehaviour
{
    public Button resetButton; // Reference to the reset button
    public Text highestScoreText; // Reference to the text displaying the highest score

    public GameObject lockedItem1; // Reference to the first locked image
    public GameObject lockedItem2; // Reference to the first locked image
    public GameObject lockedItem3; // Reference to the first locked image

    public GameObject lockedItemImage1; // Reference to the first locked image
    public GameObject lockedItemImage2; // Reference to the second locked image
    public GameObject lockedItemImage3; // Reference to the second locked image
    public GameObject lockedItemImage4; // Reference to the second locked image
    public GameObject lockedItemImage5; // Reference to the second locked image
    public GameObject lockedItemImage6; // Reference to the second locked image
    public GameObject lockedItemImage7; // Reference to the second locked image
    public GameObject lockedItemImage8; // Reference to the second locked image
    public GameObject lockedItemImage9; // Reference to the second locked image

    public Button yourButton2; // Reference to your button
    public Button yourButton3; // Reference to your button
    public Button yourButton4; // Reference to your button
    public Button yourButton5; // Reference to your button
    public Button yourButton6; // Reference to your button
    public Button yourButton7; // Reference to your button
    public Button yourButton8; // Reference to your button
    public Button yourButton9; // Reference to your button

    void Start()
    {
        // Attach a click event listener to the reset button
        resetButton.onClick.AddListener(ResetHighScoreButtonClick);
    }

    void ResetHighScoreButtonClick()
    {
        // Reset the high score to 0
        PlayerPrefs.SetInt("HighestScore", 0);

        PlayerPrefs.SetInt("LockedItem1State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedItem2State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedItem3State", 1); // Set the second locked image to active

        PlayerPrefs.SetInt("LockState", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState2", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState3", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState4", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState5", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState6", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState7", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState8", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("LockState9", 0); // Reset the lock state to locked

        PlayerPrefs.SetInt("ButtonState", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState2", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState3", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState4", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState5", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState6", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState7", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState8", 0); // Reset the button state
        PlayerPrefs.SetInt("ButtonState9", 0); // Reset the button state

        PlayerPrefs.SetInt("LockedImage1State", 1); // Set the first locked image to active
        PlayerPrefs.SetInt("LockedImage2State", 1); // Set the second locked image to active
        // PlayerPrefs.SetInt("LockedImage3State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedImage4State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedImage5State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedImage6State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedImage7State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedImage8State", 1); // Set the second locked image to active
        PlayerPrefs.SetInt("LockedImage9State", 1); // Set the second locked image to active

        // Reset the unlock message states
        PlayerPrefs.SetInt("UnlockMessageShown", 0);
        PlayerPrefs.SetInt("UnlockMessage1Shown", 0);

        PlayerPrefs.Save();

        // Update the UI text to reflect the reset
        highestScoreText.text = "Highest Score: 0";

        // Show the locked item images after resetting
        lockedItem1.SetActive(true);
        lockedItem2.SetActive(true);
        lockedItem3.SetActive(true);

        // Show the locked images after resetting
        lockedItemImage1.SetActive(true);
        lockedItemImage2.SetActive(true);
        // lockedItemImage3.SetActive(true);
        lockedItemImage4.SetActive(true);
        lockedItemImage5.SetActive(true);
        lockedItemImage6.SetActive(true);
        lockedItemImage7.SetActive(true);
        lockedItemImage8.SetActive(true);
        lockedItemImage9.SetActive(true);

        // Set the button as not clickable
        yourButton2.interactable = false;
        yourButton3.interactable = false;
        yourButton4.interactable = false;
        yourButton5.interactable = false;
        yourButton6.interactable = false;
        yourButton7.interactable = false;
        yourButton8.interactable = false;
        yourButton9.interactable = false;
    }
}