using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    // Function to go back to the main map scene.
    public void GoBackToMainMap()
    {
        SceneManager.LoadScene("MainScene"); // Replace "MainMapScene" with the name of your main map scene.
    }
}
