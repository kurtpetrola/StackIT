using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            LifeManager1.Instance.DetectBox();
        }
    }
}