using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager2 : MonoBehaviour
{
    public static LifeManager2 Instance;
    public int lives = 3;
    public LifeUIManager2 lifeUIManager2; // Reference to the LifeUIManager script

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

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {

    }
}