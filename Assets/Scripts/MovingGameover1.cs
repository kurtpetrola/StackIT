using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGameover1 : MonoBehaviour
{
    public float minX = -.5f; // Minimum X position
    public float maxX = .5f; // Maximum X position
    public float moveSpeed = 2f; // Speed of movement
    public float moveUpSpeed = 2f; // The speed at which the platform moves up
    public float moveUpDistance = 2f;
     bool moveRight = true; // Initial movement direction

    void Update()
    {
        MovePlatform();
    }
public void MoveUp()
{
    Vector3 newPosition = transform.position;
    newPosition.y += moveUpDistance;
    StartCoroutine(MoveToPosition(newPosition));
}

// Coroutine to smoothly move the platform up
private IEnumerator MoveToPosition(Vector3 targetPosition)
{
    while (Vector3.Distance(transform.position, targetPosition) > 1f)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveUpSpeed * Time.deltaTime);
        yield return null;
    }
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