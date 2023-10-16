using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager Instance;
    //public Text scoreText;
    private int playerScore = 0;

    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    // This method is called when you want to display the game over screen with the score.
    public void ShowGameOverUI(int score)
    {
        // Display the game over screen elements.
        gameOverPanel.SetActive(true);

        // Set the points text to display the player's score.
        //scoreText.text = "Score: " + score.ToString();
        //GameOverUIManager.Instance.ShowGameOverUI(playerScore);
    }

    public void RestartGame()
    {
        // Restart the game by reloading the current scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene(
             UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void GoToMainMap()
    {
        // Load the main map scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    // This method is called when the player scores points during the game.
    public void IncreaseScore()
    {
        playerScore++;
    }
}
