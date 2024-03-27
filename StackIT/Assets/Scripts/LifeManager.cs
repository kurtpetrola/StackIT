using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    public int lives = 1;
    public LifeUIManager lifeUIManager; // Reference to the LifeUIManager script

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (lifeUIManager != null)
        {
            lifeUIManager.UpdateHeartIcons();
        }
    }

    public void DecreaseLife()
    {
        lives--;

        if (lifeUIManager != null)
        {
            lifeUIManager.UpdateHeartIcons();
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