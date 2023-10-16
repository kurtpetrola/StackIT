using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text unlockText;
    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;

    // Reference to the ShopManager script
    //public ShopManager shopManager;

    public void IncreaseScore()
    {
        playerScore++;

        // Check if an item is stacked
        if (playerScore == 4)
        {
            stackedItems++;
        }

        playerScore += 1 * stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            StartCoroutine(ShowUnlockMessage());
        }
    }

    private IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.text = "2X Activated";
        unlockText.gameObject.SetActive(true);

        // Call the ShopManager to unlock the 2x item
        //shopManager.Unlock2xItem();

        yield return new WaitForSeconds(5f);
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }
}
