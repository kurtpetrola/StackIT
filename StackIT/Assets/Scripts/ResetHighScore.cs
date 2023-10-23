using UnityEngine;
using UnityEngine.UI;

public class ResetHighScore : MonoBehaviour
{
    public Button resetButton; // Reference to the reset button
    public Text highestScoreText; // Reference to the text displaying the highest score
    public GameObject lockedItemImage1; // Reference to the first locked image
    public GameObject lockedItemImage2; // Reference to the second locked image
    public Button yourButton; // Reference to your button

    void Start()
    {
        // Attach a click event listener to the reset button
        resetButton.onClick.AddListener(ResetHighScoreButtonClick);
    }

    void ResetHighScoreButtonClick()
    {
        // Reset the high score to 0
        PlayerPrefs.SetInt("HighestScore", 0);
        PlayerPrefs.SetInt("LockState", 0); // Reset the lock state to locked
        PlayerPrefs.SetInt("ButtonState", 0); // Reset the button state
        PlayerPrefs.SetInt("LockedImage1State", 1); // Set the first locked image to active
        PlayerPrefs.SetInt("LockedImage2State", 1); // Set the second locked image to active
        PlayerPrefs.Save();

        // Update the UI text to reflect the reset
        highestScoreText.text = "Highest Score: 0";

        // Show the locked images after resetting
        lockedItemImage1.SetActive(true);
        lockedItemImage2.SetActive(true);

        // Set the button as not clickable
        yourButton.interactable = false;
    }
}

