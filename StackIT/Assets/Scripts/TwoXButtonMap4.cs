using UnityEngine;
using UnityEngine.UI;

public class TwoXButtonMap4 : MonoBehaviour
{
    public Text scoreText;
    public Button twoXButton1;
    private bool buttonActive = false;
    private float countdown = 60f;

    void Start()
    {
        twoXButton1.onClick.AddListener(ActivateTwoX);
    }

    void Update()
    {
        if (buttonActive)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                twoXButton1.interactable = false;
                buttonActive = false;
                Destroy(gameObject);
            }
        }
    }

    void ActivateTwoX()
    {
        buttonActive = true;
        twoXButton1.interactable = false; // Assuming the button will disappear after activation
    }

    public bool IsButtonActive()
    {
        return buttonActive;
    }

    public void IncreaseScore()
    {
        if (buttonActive)
        {
            // Increase the score by 2 for each successful drop
            // Example: scoreText.text = "Score: " + (int.Parse(scoreText.text.Split(' ')[1]) + 2);
        }
        else
        {
            // Increase the score by 1 for each successful drop
            // Example: scoreText.text = "Score: " + (int.Parse(scoreText.text.Split(' ')[1]) + 1);
        }
    }
}