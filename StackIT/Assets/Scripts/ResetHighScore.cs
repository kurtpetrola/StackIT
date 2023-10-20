using UnityEngine;
using UnityEngine.UI;

public class ResetHighScore : MonoBehaviour
{
    public Button resetButton; // Reference to the reset button
    public Text highestScoreText; // Reference to the text displaying the highest score
    public GameObject lockedItemImage; // Reference to the locked image in the shop

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
        PlayerPrefs.Save();

        // Update the UI text to reflect the reset
        highestScoreText.text = "Highest Score: 0";

        // Show the locked image (set it to active) after resetting
        lockedItemImage.SetActive(true);
    }
}
