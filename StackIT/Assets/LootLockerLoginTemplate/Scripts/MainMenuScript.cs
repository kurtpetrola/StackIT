// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement;
// using LootLocker.Requests;

// public class MainMenuScript : MonoBehaviour
// {

//   private string loginSceneName = "WhiteLabelAndGuestLogin";
//   private string leaderboardSceneName = "LeaderboardScreen";
//   private string playGameSceneName = "OnlineMode";

//   [Header("Player Name")]
//   public TextMeshProUGUI playerNameText;

//   [Header("Animators")]
//   public CanvasAnimator mainMenuAnimator;

//   [Header("Error Handling")]
//   public TextMeshProUGUI errorText;
//   public GameObject errorPanel;
//   public Animator errorScreenAnimator;

//   public void Start()
//   {

//     // check if they have player id
//     string playerId = PlayerPrefs.GetString("LLplayerId", "nada");
//     if (playerId == "nada")
//     {
//       onNoPLayerId();
//       return;
//     }

//     //get player name
//     LootLockerSDKManager.GetPlayerName((response) =>
//     {
//       if (response.success)
//       {
//         playerNameText.text = response.name;
//       }
//     });

//     SubmitLeaderboardScore.GetPlayerHighScore();

//     mainMenuAnimator.CallAppearOnAllAnimators();

//   }

//   public void onNoPLayerId()
//   {
//     showErrorMessage("You're not logged in");
//     //wait 3 seconds and then logout 
//     Invoke("goBackToLogin", 3);
//   }

//   public void goBackToLogin()
//   {
//     SceneManager.LoadScene(loginSceneName);
//   }

//   public void goToLeaderboard()
//   {
//     SceneManager.LoadScene(leaderboardSceneName);
//   }

//   public void startGame()
//   {
//     SceneManager.LoadScene(playGameSceneName);
//   }

//   // Show an error message on the screen
//   public void showErrorMessage(string message, int showTime = 3)
//   {
//     //set active
//     errorPanel.SetActive(true);
//     errorText.text = message.ToUpper();
//     errorScreenAnimator.SetTrigger("Show");
//     //wait for 3 seconds and hide the error panel
//     Invoke("hideErrorMessage", showTime);
//   }

//   private void hideErrorMessage()
//   {
//     errorScreenAnimator.SetTrigger("Hide");
//   }

//   public void logout()
//   {
//     //remove the auto remember
//     PlayerPrefs.SetInt("rememberMe", 0);

//     //end the session
//     LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();

//     LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
//       {
//         if (!response.success)
//         {
//           showErrorMessage("Error logging out");
//           return;
//         }
//         PlayerPrefs.DeleteKey("LLplayerId");
//         LootLockerSDKManager.ClearLocalSession();
//         SceneManager.LoadScene(loginSceneName);
//         Debug.Log("Logged Out");
//       });

//   }
// }
