using UnityEngine;
using UnityEngine.UI;

public class ResetHighScore : MonoBehaviour
{
    public Button resetButton; // Reference to the reset button
    public Text highestScoreText; // Reference to the text displaying the highest score

    void Start()
    {
        // Attach a click event listener to the reset button
        resetButton.onClick.AddListener(ResetHighScoreButtonClick);
    }

    void ResetHighScoreButtonClick()
    {
        // Reset the high score to 0
        PlayerPrefs.SetInt("HighestScore", 0);
        PlayerPrefs.Save();

        // Update the UI text to reflect the reset
        highestScoreText.text = "Highest Score: 0";
    }
}
