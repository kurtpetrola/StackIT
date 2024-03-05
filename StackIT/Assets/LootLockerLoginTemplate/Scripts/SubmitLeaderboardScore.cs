// using UnityEngine;
// using LootLocker.Requests;

// public class SubmitLeaderboardScore : MonoBehaviour
// {
//   public static string leaderboardKey = "stackItUPang";

//   public static void Submit(int scoreToSubmit)
//   {
//     //the member id is set when the user logs in or uses guest login, if they have not done either of those then this will be empty and the request will fail.
//     // we set it throughout WhiteLabelManager.cs, you can cmd/ctrl + f and look for PlayerPrefs.SetString("LLplayerId"
//     string playerId = PlayerPrefs.GetString("LLplayerId");
//     LootLockerSDKManager.SubmitScore(playerId, scoreToSubmit, leaderboardKey, (response) =>
//     {
//       if (response.success)
//       {
//         Debug.Log("SubmitLeaderboardScore successful");
//       }
//       else
//       {
//         Debug.LogError("SubmitLeaderboardScore failed");
//         Debug.LogError("Error: " + response.error);
//       }
//     });
//   }

//   public static void GetPlayerHighScore()
//   {
//     string playerId = PlayerPrefs.GetString("LLplayerId");
//     LootLockerSDKManager.GetMemberRank(leaderboardKey, playerId, (response) =>
//       {
//         if (response.statusCode == 200)
//         {
//           Debug.Log("Successful");
//           Debug.Log("Player Score: " + response.score);
//           PlayerPrefs.SetInt("highScore", response.score);
//         }
//         else
//         {
//           Debug.Log("failed to get highscore: " + response.error);
//         }
//       });
//   }
// }
