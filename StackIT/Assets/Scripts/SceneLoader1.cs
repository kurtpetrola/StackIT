/*using UnityEngine;
using UnityEngine.UI;

public class SceneLoader1 : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public float delayBeforeStart = 1.0f; // Delay before starting the loading
    public float progressMultiplier = 0.1f; // Adjust this to control the progress speed

    public ScoreManager scoreManager; // Reference to the ScoreManager script
    public int sceneIndexToLoad = 1; // Set the scene index you want to load

    private Button sceneLoaderButton;

    private void Start()
    {
        sceneLoaderButton = GetComponent<Button>();
        sceneLoaderButton.interactable = false; // Initially disable the button
    }

    private void Update()
    {
        CheckScoreAndEnableButton();
    }

    private void CheckScoreAndEnableButton()
    {
        int playerScore = scoreManager.GetPlayerScore();

        if (playerScore >= 3)
        {
            sceneLoaderButton.interactable = true; // Enable the button when the score reaches 3
        }
    }

    public void LoadSceneOnClick()
    {
        // Load the specified scene using the SceneLoadingManager
        SceneLoadingManager sceneLoadingManager = FindObjectOfType<SceneLoadingManager>();
        StartCoroutine(sceneLoadingManager.LoadSceneAsynchronously(sceneIndexToLoad, delayBeforeStart));
    }
}
*/
