using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StartPanel : MonoBehaviour
{
    public GameObject panel;
    public Text countdownText;
    private float countdown = 11f;
    public bool panelClosed = false;

    private GameplayController gameplayController;

    void Start()
    {
        panel.SetActive(true); // Ensure the panel is visible when the game starts
        gameplayController = GameplayController.instance; // Get reference to the GameplayController
        InvokeRepeating("UpdateCountdown", 0f, 1f); // Call the UpdateCountdown function every second
    }

    // Function to update the countdown display
    void UpdateCountdown()
    {
        countdown -= 1f;
        countdownText.text = Mathf.Ceil(countdown).ToString(); // Update the countdown text
        if (countdown <= 0 && !panelClosed)
        {
            panel.SetActive(false); // Hide the panel after the countdown finishes
            countdownText.gameObject.SetActive(false); // Hide the countdown text
            panelClosed = true;
            gameplayController.SpawnNewBox(); // Call the function in GameplayController to spawn the box
            CancelInvoke("UpdateCountdown"); // Stop the countdown
        }
    }

    // Function to call when the panel is closed manually (if needed)
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