using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public GameObject panel;
    public Text countdownText;
    private float countdown = 11f;
    public bool panelClosed = false;

    private GameplayController gameplayController;
    private bool countdownPaused = false;
    private bool isNetworkDisconnected = false; // To track network status

    void Start()
    {
        panel.SetActive(true);
        gameplayController = GameplayController.instance;
        InvokeRepeating("UpdateCountdown", 0f, 1f);
    }

    void UpdateCountdown()
    {
        if (!countdownPaused && !panelClosed)
        {
            countdown -= 1f;
            countdownText.text = Mathf.Ceil(countdown).ToString();
            if (countdown < 1)
            {
                panel.SetActive(false);
                countdownText.gameObject.SetActive(false);
                panelClosed = true;
                gameplayController.SpawnNewBox();
                CancelInvoke("UpdateCountdown");
            }
        }
    }

    public void PauseCountdown(bool paused)
    {
        countdownPaused = paused;
    }

    public void ResumeCountdown()
    {
        countdownPaused = false;
    }

    public void ContinueCountdown()
    {
        if (!panelClosed && isNetworkDisconnected)
        {
            countdownPaused = false;
            countdown = 11f;
            countdownText.text = Mathf.Ceil(countdown).ToString();
            panel.SetActive(true);
            countdownText.gameObject.SetActive(true);
            // InvokeRepeating("UpdateCountdown", 0f, 1f);
            isNetworkDisconnected = false;
        }
    }

    public void OnNetworkDisconnected()
    {
        PauseCountdown(true);
        isNetworkDisconnected = true;
    }

    public void OnNetworkReconnected()
    {
        ContinueCountdown();
    }

    public void ClosePanelManually()
    {
        if (!panelClosed)
        {
            panel.SetActive(false);
            countdownText.gameObject.SetActive(false);
            panelClosed = true;
            gameplayController.SpawnNewBox(); // Call the function in GameplayController to spawn the box
            CancelInvoke("UpdateCountdown"); // Stop the countdown
        }
    }
}