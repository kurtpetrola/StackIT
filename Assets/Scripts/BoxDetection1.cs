using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            LifeManager2.Instance.DetectBox();
        }
    }
}