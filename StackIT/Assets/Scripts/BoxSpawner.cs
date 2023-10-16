using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] boxPrefabs; 

    public void SpawnBox()
    {
        int randomIndex = Random.Range(0, boxPrefabs.Length);
        GameObject boxPrefabVariant = boxPrefabs[randomIndex];

        GameObject boxObj = Instantiate(boxPrefabVariant);
        Vector3 temp = transform.position;
        temp.z = 2f;
        boxObj.transform.position = temp;
    }
}