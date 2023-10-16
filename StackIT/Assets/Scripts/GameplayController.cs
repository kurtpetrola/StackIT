using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public BoxSpawner box_Spawner;
    [HideInInspector]
    public BoxScript currentBox;
    public CameraFollow cameraScript;
    private int moveCount;
    public GameObject[] objectsToDrop;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        box_Spawner.SpawnBox();
    }

    void Update()
    {
        DetectInput();
    }

    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int randomIndex = UnityEngine.Random.Range(0, objectsToDrop.Length);
            GameObject objectToDrop = objectsToDrop[randomIndex];

            currentBox.DropRandomObject();
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", .7f);
    }

    void NewBox()
    {
        box_Spawner.SpawnBox();
    }

    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 2)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 2f;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
