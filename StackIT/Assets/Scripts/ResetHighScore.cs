using UnityEngine;
using UnityEngine.UI;

public class ResetHighScore : MonoBehaviour
{
    // UI references
    public Button resetButton;
    public Text highestScoreText;

    // Locked items and images references
    public GameObject[] lockedItems = new GameObject[3];
    public GameObject[] lockedItemImages = new GameObject[9];

    // Buttons references
    public Button[] buttons = new Button[9];

    private const string HighestScoreKey = "HighestScore";
    private const string LockedItemStateKey = "LockedItem{0}State";
    private const string LockStateKey = "LockState{0}";
    private const string ButtonStateKey = "ButtonState{0}";
    private const string LockedImageStateKey = "LockedImage{0}State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage1ShownKey = "UnlockMessage1Shown";

    void Start()
    {
        // Attach a click event listener to the reset button
        resetButton.onClick.AddListener(ResetHighScoreButtonClick);
    }

    void ResetHighScoreButtonClick()
    {
        // Reset the high score
        PlayerPrefs.SetInt(HighestScoreKey, 0);

        // Reset states for locked items, lock states, button states, and locked images
        for (int i = 1; i <= lockedItems.Length; i++)
        {
            PlayerPrefs.SetInt(string.Format(LockedItemStateKey, i), 1);
        }

        for (int i = 0; i < lockedItemImages.Length; i++)
        {
            PlayerPrefs.SetInt(string.Format(LockStateKey, i + 1), 0);
            PlayerPrefs.SetInt(string.Format(ButtonStateKey, i + 1), 0);
            PlayerPrefs.SetInt(string.Format(LockedImageStateKey, i + 1), 1);
        }

        // Reset unlock message states
        PlayerPrefs.SetInt(UnlockMessageShownKey, 0);
        PlayerPrefs.SetInt(UnlockMessage1ShownKey, 0);

        PlayerPrefs.Save();

        // Update the UI text to reflect the reset
        highestScoreText.text = "Highest Score: 0";

        // Activate locked item images and deactivate buttons
        foreach (var item in lockedItems)
        {
            item.SetActive(true);
        }

        foreach (var image in lockedItemImages)
        {
            image.SetActive(true);
        }

        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }
}
