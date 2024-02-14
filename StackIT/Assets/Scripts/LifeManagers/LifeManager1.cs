using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager1 : MonoBehaviour
{
    public static LifeManager1 Instance;
    public int lives = 2;
    public LifeUIManager1 lifeUIManager1; // Reference to the LifeUIManager script

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (lifeUIManager1 != null)
        {
            lifeUIManager1.UpdateHeartIcons();
        }
    }

    public void DecreaseLife()
    {
        lives--;

        if (lifeUIManager1 != null)
        {
            lifeUIManager1.UpdateHeartIcons();
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {

    }
}