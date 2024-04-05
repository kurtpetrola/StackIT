using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management functions

public class PauseManager : MonoBehaviour
{
    public GameObject quitConfirmationPanel; // Assign this in the inspector

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the quit confirmation panel is not active when the scene starts
        quitConfirmationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the Back button press on Android devices
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        // Pause the game by setting time scale to 0
        Time.timeScale = 0;
        // Activate the quit confirmation panel
        quitConfirmationPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        // Resume the game by setting time scale back to 1
        Time.timeScale = 1;
        // Deactivate the quit confirmation panel
        quitConfirmationPanel.SetActive(false);
    }

    public void ConfirmQuit()
    {
        // Optional: Perform any cleanup before quitting
        // ...

        // Resume the time before quitting to the main menu or exiting
        Time.timeScale = 1;

        // Load the main menu scene or quit the application
        //SceneManager.LoadScene("MainScene"); // Uncomment and set your main menu scene name
        // or
        Application.Quit();
    }
}
