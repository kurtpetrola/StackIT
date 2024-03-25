using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManagerMap9 : MonoBehaviour
{
    public FourXButtonMap9 fourXButtonScriptMap9;
    public Text scoreText;
    public Text unlockText9; // Reference to the second unlock text
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage10; // Reference to the second locked image
    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing9 = false; // Track the second unlock message

    // PlayerPrefs keys
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage10StateKey = "LockedImage10State";
    private const string UnlockMessage9ShownKey = "UnlockMessage9Shown";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        // Check if unlock message has been shown before
        isUnlockMessageShowing9 = PlayerPrefs.GetInt(UnlockMessage9ShownKey, 0) == 1;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void IncreaseScore()
    {
        playerScore++;


        if (playerScore == 3)
        {
            // Remove lockedItemImage when the player score reaches 3
            lockedItemImage.SetActive(false);
            PlayerPrefs.SetInt(LockedImageStateKey, 0);
            PlayerPrefs.Save();
        }

        if (playerScore == 30)
        {
            // Remove lockedItemImage10 when the player score reaches 45
            lockedItemImage10.SetActive(false);
            PlayerPrefs.SetInt(LockedImage10StateKey, 0);
            PlayerPrefs.Save();

            if (!isUnlockMessageShowing9)
            {
                StartCoroutine(ShowUnlockMessage9());
                unlockText9.text = "";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage9ShownKey, 1);
                PlayerPrefs.Save();
            }
        }

        if (fourXButtonScriptMap9.IsButtonActive())
        {
            playerScore += 3;
        }

        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
    }

    public void DecreaseScore()
    {
        playerScore -= 1;
        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
    }



    private System.Collections.IEnumerator ShowUnlockMessage9()
    {
        isUnlockMessageShowing9 = true;
        unlockText9.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText9.gameObject.SetActive(false);
        isUnlockMessageShowing9 = false;
    }
}