using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager2 : MonoBehaviour
{
    public static LifeManager2 Instance;
    public int lives = 3;
    public LifeUIManager2 lifeUIManager2; // Reference to the LifeUIManager script
    private int boxesDetected = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (lifeUIManager2 != null)
        {
            lifeUIManager2.UpdateHeartIcons();
        }
    }

    public void DecreaseLife()
    {
        lives--;

        if (lifeUIManager2 != null)
        {
            lifeUIManager2.UpdateHeartIcons();
        }

        // if (lives <= 0)
        // {
        //     GameOver();
        // }
    }

    public void DetectBox()
    {
        boxesDetected++;

        if (boxesDetected == 2)
        {

            GameOver();// Reset box count after decreasing life
        }
        if (lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {

    }
}