using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public BoxSpawner box_Spawner;
    public StartPanel startPanel;
    [HideInInspector]
    public BoxScript currentBox;
    public BoxScript2 currentBox2;
    public BoxScript3 currentBox3;
    public BoxScript4 currentBox4;
    public BoxScript5 currentBox5;
    public BoxScript6 currentBox6;
    public BoxScript7 currentBox7;
    public BoxScript8 currentBox8;
    public BoxScript9 currentBox9;
    public NormalBoxScript currentBoxNormal;
    public BoxScriptOnline currentBoxOnline;
    public FakeBox fakeBox;
    public MovingGameover movingGameoverScript;
    public MovingGameover movingGameoverScript1;

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
        if (startPanel.panelClosed) // Check if the panel is closed
        {
            box_Spawner.SpawnBox();
        }
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
            else if (currentBox6 != null)
                currentBox6.DropRandomObject();
            else if (currentBox7 != null)
                currentBox7.DropRandomObject();
            else if (currentBox8 != null)
                currentBox8.DropRandomObject();
            else if (currentBox9 != null)
                currentBox9.DropRandomObject();
            else if (currentBoxNormal != null)
                currentBoxNormal.DropRandomObject();
                else if (currentBoxOnline != null)
                currentBoxOnline.DropRandomObject();
            else if (fakeBox != null)
                fakeBox.DropRandomObject();
        }
    }

    public void SpawnNewBox()
    {

        box_Spawner.SpawnBox();

    }

    void NewBox()
    {
        box_Spawner.SpawnBox();
        // Set the spawned box as the current box

    }

    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 3)
        {
            moveCount = 0;
            cameraScript.targetPos.y += 1.8f;

            // If there is a movingGameoverScript attached, move it up as well.
            if (movingGameoverScript != null)
            {
                movingGameoverScript.MoveUp();
            }

            if (movingGameoverScript1 != null)
            {
                movingGameoverScript1.MoveUp();
            }
        }
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}