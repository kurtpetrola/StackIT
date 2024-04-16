// using UnityEngine;
// using Firebase.Database;
// using System;
// using System.Collections.Generic;
// using System.Collections;

// [Serializable]
// public class dataToSave
// {
//     public string userName;
//     public List<string> names; // List of player names
//     public List<string> time;  // List of player times in "00:00" format

//     public dataToSave()
//     {
//         names = new List<string>();
//         time = new List<string>();
//     }
// }

// public class DataSaver : MonoBehaviour
// {
//     public dataToSave dts;
//     public string userId;
//     DatabaseReference dbref;
//     public Action onDataLoaded; // Callback function to be invoked after data is loaded

//     private void Awake()
//     {
//         dbref = FirebaseDatabase.DefaultInstance.RootReference;
//     }

//     public void SaveDataFn()
//     {
//         string json = JsonUtility.ToJson(dts);
//         dbref.Child("users").Child(userId).SetRawJsonValueAsync(json);
//     }

//     public void LoadDataFn()
//     {
//         StartCoroutine(LoadDataCoroutine());
//     }

//     private IEnumerator LoadDataCoroutine()
//     {
//         var serverData = dbref.Child("users").Child(userId).GetValueAsync();
//         yield return new WaitUntil(() => serverData.IsCompleted);

//         DataSnapshot snapshot = serverData.Result;
//         string jsonData = snapshot.GetRawJsonValue();

//         if (jsonData != null)
//         {
//             dts = JsonUtility.FromJson<dataToSave>(jsonData);
//             onDataLoaded?.Invoke();
//         }
//         else
//         {
//             Debug.LogWarning("Failed to load data from Firebase.");
//         }
//     }



// }
