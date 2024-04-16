using UnityEngine;
using UnityEngine.UI;

public class FourXButtonMap9 : MonoBehaviour
{
    public Text scoreText;
    public Button fourXButton1;
    public Text countdownText; // Text element to show the countdown
    private bool buttonActive = false;
    private float countdown = 60f;

    void Start()
    {
        fourXButton1.onClick.AddListener(ActivateFourX);
        countdownText.gameObject.SetActive(false); // Initially hide the countdown text
    }

    void Update()
    {
        if (buttonActive)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
                countdownText.text = " " + countdown.ToString("F1"); // Update countdown text with one decimal point
            }
            else
            {
                buttonActive = false;
                fourXButton1.interactable = true; // Make the button interactable again
                countdownText.gameObject.SetActive(false); // Hide the countdown text
                Destroy(gameObject);
            }
        }
    }

    void ActivateFourX()
    {
        buttonActive = true;
        countdown = 60f; // Reset the countdown to 60 seconds
        fourXButton1.interactable = false; // Disable the button as it's been activated
        countdownText.gameObject.SetActive(true); // Show the countdown text
    }

    public bool IsButtonActive()
    {
        return buttonActive;
    }

    public void IncreaseScore()
    {
        if (buttonActive)
        {
            // Increase the score by 4 for each successful action
            // Example: scoreText.text = "Score: " + (int.Parse(scoreText.text.Split(' ')[1]) + 4);
        }
        else
        {
            // Increase the score by 1 for each successful action
            // Example: scoreText.text = "Score: " + (int.Parse(scoreText.text.Split(' ')[1]) + 1);
        }
    }
}