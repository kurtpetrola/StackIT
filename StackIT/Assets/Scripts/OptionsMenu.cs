using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Button optionsButton;
    public OptionManager optionManager; // Reference to the GameManager script.

    public void ToggleOptionsPanel()
    {
        bool isOpen = !optionsPanel.activeSelf;

        optionsPanel.SetActive(isOpen);

        if (isOpen)
        {
            optionManager.PauseGame(); // Pause the game when opening the options.
        }
        else
        {
            optionManager.ResumeGame(); // Resume the game when closing the options.
        }
    }

    public void ResumeGame()
    {
        optionsPanel.SetActive(false); // Hide the Options Panel.
        optionManager.ResumeGame(); // Resume the game.
    }

    public void ExitGame()
    {
        // Load the main map scene by its name.
        SceneManager.LoadScene("MainScene"); // Replace "MainMapScene" with the actual name of your main map scene.
    }
}
