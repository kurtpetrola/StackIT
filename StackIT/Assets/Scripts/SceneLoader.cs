using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public float delayBeforeStart = 1.0f; // Delay before starting the loading
    public float progressMultiplier = 0.1f; // Adjust this to control the progress speed

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        // Delay for a specified time before showing the loading screen
        yield return new WaitForSeconds(delayBeforeStart);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            // Slow down the progress by multiplying it by a smaller factor
            slider.value += progress * progressMultiplier;
            progressText.text = (slider.value * 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
