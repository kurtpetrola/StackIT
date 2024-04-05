using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float minX = -.5f; // Minimum X position
    public float maxX = .5f; // Maximum X position
    public float moveSpeed = 1f; // Speed of movement

    bool moveRight = true; // Initial movement direction

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (moveRight)
        {
            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > maxX)
            {
                moveRight = false;
            }

            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.x -= moveSpeed * Time.deltaTime;

            if (temp.x < minX)
            {
                moveRight = true;
            }

            transform.position = temp;
        }
    }
}