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
    public BoxScript2 currentBox2;
    public BoxScript3 currentBox3;
    public BoxScript4 currentBox4;
    public BoxScript5 currentBox5;
    public FakeBox fakeBox;

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

            if (currentBox != null)
                currentBox.DropRandomObject();
            else if (currentBox2 != null)
                currentBox2.DropRandomObject();
            else if (currentBox3 != null)
                currentBox3.DropRandomObject();
            else if (currentBox4 != null)
                currentBox4.DropRandomObject();
                 else if (currentBox5 != null)
                currentBox5.DropRandomObject();
            else if (fakeBox != null)
                fakeBox.DropRandomObject();
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", .7f);
    }

    void NewBox()
    {
        box_Spawner.SpawnBox();
        // Set the spawned box as the current box
        
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