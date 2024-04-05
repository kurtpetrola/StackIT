using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    public GameObject boxCollider; // Assign your Box Collider 2D GameObject here
    public GameOverUIManager gameOverUIManager;

    private bool gameStarted = false;
    private bool boxDetected = false;
    private int playerScore = 0;

    void Update()
    {
        if (!gameStarted)
        {
            if (IsBoxDetected())
            {
                gameStarted = true;
            }
        }
        else
        {
            if (!IsBoxDetected() && !boxDetected)
            {
                // No box detected after game start, trigger game over
                gameOverUIManager.ShowGameOverUI(playerScore);
                boxDetected = true; // Prevent showing game over UI multiple times
            }
        }
    }

    bool IsBoxDetected()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.GetComponent<BoxCollider2D>().size, 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Box")
            {
                return true;
            }
        }

        return false;
    }
}