using System.Collections;
using UnityEngine;

public class NetworkCheck : MonoBehaviour
{
    public GameObject noConnectionPanel;
    private bool isGamePaused = false;
    private bool previouslyNotReachable = false;
    public GameObject loadingIndicator;

    private WaitForSeconds waitDelay = new WaitForSeconds(3f);

    private void Start()
    {
        StartCoroutine(CheckInternetConnection());
        StartCoroutine(RunYieldContinuously());
    }

    IEnumerator CheckInternetConnection()
    {
        bool isConnected = true;
        yield return null;

        while (true)
        {
            bool isNotReachable = Application.internetReachability == NetworkReachability.NotReachable;

            if (isNotReachable && isConnected)
            {
                noConnectionPanel.SetActive(true);
                loadingIndicator.SetActive(true);
                PauseGame();
                isConnected = false;
            }
            else if (!isNotReachable && !isConnected)
            {
                noConnectionPanel.SetActive(false);
                loadingIndicator.SetActive(false);
                ResumeGame();
                isConnected = true;
                StartCoroutine(RunYieldContinuously());
            }


            yield return null;
        }
    }

    IEnumerator RunYieldContinuously()
    {
        while (true)
        {
            yield return null;
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;

        // Implement additional logic to freeze game elements if needed
    }

    void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;

        // Implement additional logic to resume game elements if needed
    }
}