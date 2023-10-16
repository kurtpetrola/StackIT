using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -2.2f, max_X = 2.2f;
    private int playerScore = 0;
    private int landedBoxCount = 0;
    private int consecutiveSuccessCount = 0; // Track consecutive successful landings
    private bool canMove;
    private float move_Speed = 2.5f;
    public GameObject[] objectsToDrop;
    private Rigidbody2D myBody;
    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;
    private ScoreManager scoreManager;
    private float currentBoxMoveSpeed = 5f;
    private GameObject lastDroppedItem;
    private bool droppedTwoItemsInSuccession;
    private int successfulLandings = 0;
    private int itemsDropped = 0;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;

        // Find the ScoreManager script in the scene.
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager script not found in the scene.");
        }
    }

    void Start()
    {
        canMove = true;

        if (Random.Range(0, 2) > 0)
        {
            move_Speed = 4f;
        }

        currentBoxMoveSpeed = move_Speed;

        GameplayController.instance.currentBox = this;
    }

    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += move_Speed * Time.deltaTime;

            if (temp.x > max_X)
            {
                move_Speed *= -1f;
            }
            else if (temp.x < min_X)
            {
                move_Speed *= -1f;
            }
            transform.position = temp;
        }
    }

    public void DropRandomObject()
    {
        canMove = false;
        myBody.gravityScale = Random.Range(2, 4);

        if (objectsToDrop.Length > 0)
        {
            int randomIndex = Random.Range(0, objectsToDrop.Length);
            GameObject objectToDrop = objectsToDrop[randomIndex];
            GameObject spawnedObject = Instantiate(objectToDrop, transform.position, Quaternion.identity);

            if (spawnedObject.GetComponent<Rigidbody2D>() != null)
            {
                Rigidbody2D objRigidbody = spawnedObject.GetComponent<Rigidbody2D>();
                objRigidbody.velocity = new Vector2(currentBoxMoveSpeed, objRigidbody.velocity.y);

                if (lastDroppedItem != null)
                {
                    droppedTwoItemsInSuccession = true;
                }
                lastDroppedItem = spawnedObject;
            }
        }

        itemsDropped++;

        if (itemsDropped == 5)
        {
            successfulLandings++; // Increment successful landings
            itemsDropped = 0; // Reset items dropped

            if (successfulLandings == 5)
            {
                playerScore *= 2; // Double the score
                successfulLandings = 0; // Reset the count
            }
        }
    }

    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;
        playerScore++;

        // Update the score using the ScoreManager.
        scoreManager.IncreaseScore();
        GameOverUIManager.Instance.IncreaseScore();

        landedBoxCount++;
        consecutiveSuccessCount++;

        if (consecutiveSuccessCount == 5)
        {
            playerScore *= 2; // Double the score
            consecutiveSuccessCount = 0; // Reset the count
        }

        droppedTwoItemsInSuccession = false;
        itemsDropped = 0;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }

        if (target.gameObject.tag == "Box")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;

        if (target.tag == "GameOver")
        {
            gameOver = true;
            canMove = false;
            ignoreTrigger = true;

            GameOverUIManager.Instance.ShowGameOverUI(playerScore);
        }
    }
}
