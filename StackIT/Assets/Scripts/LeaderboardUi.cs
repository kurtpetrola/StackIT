// using UnityEngine;
// using TMPro;
// using Firebase.Database;
// using System.Collections.Generic;
// using UnityEngine.UI;
// using System.Collections;

// public class LeaderboardUI : MonoBehaviour
// {
//     public List<TextMeshProUGUI> namesTextList;
//     public List<TextMeshProUGUI> timesTextList;
//     public TMP_InputField nameInputField;
//     public Button submitButton;
//     private Timer timer; 
//     public DataSaver dataSaver;

//     DatabaseReference reference;
//     bool canSubmit = true;

//     private void Start()
//     {
//         reference = FirebaseDatabase.DefaultInstance.RootReference;
//         FetchLeaderboardData();
//         timer = FindObjectOfType<Timer>();
//     }

//     void FetchLeaderboardData()
//     {
//         dataSaver.LoadDataFn();
//         for (int i = 0; i < namesTextList.Count; i++)
//         {
//             if (i < dataSaver.dts.names.Count)
//             {
//                 namesTextList[i].text = dataSaver.dts.names[i];
//             }
//             else
//             {
//                 namesTextList[i].text = "";
//             }
//         }

//         for (int i = 0; i < timesTextList.Count; i++)
//         {
//             if (i < dataSaver.dts.time.Count)
//             {
//                 timesTextList[i].text = dataSaver.dts.time[i].ToString();
//             }
//             else
//             {
//                 timesTextList[i].text = "";
//             }
//         }
//     }

//     public void SubmitTime()
//     {
//         if (!canSubmit)
//         {
//             Debug.LogWarning("You can only submit data once every 5 minutes.");
//             return;
//         }

    
//         if (timer != null && timer.isTimerRunning)
//         {
//             string formattedTime = FormatTime(timer.elapsedTime);
//             string playerName = nameInputField.text;

//             if (string.IsNullOrEmpty(playerName))
//             {
//                 Debug.LogWarning("Please enter your name.");
//                 return;
//             }

//             dataSaver.dts.names.Add(playerName);
//             dataSaver.dts.time.Add(formattedTime);

          
//             dataSaver.SaveDataFn();

//             FetchLeaderboardData();

//             StartCoroutine(SubmitCooldown());
//         }
//         else
//         {
//             Debug.LogWarning("Timer is not running.");
//         }
//     }

//     IEnumerator SubmitCooldown()
//     {
//         canSubmit = false;
//         yield return new WaitForSeconds(300f);

//         canSubmit = true;
//     }


//     private string FormatTime(float timeInSeconds)
//     {
//         int hours = Mathf.FloorToInt(timeInSeconds / 3600);
//         int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
//         int seconds = Mathf.FloorToInt(timeInSeconds % 60);

//         return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
//     }

// }
