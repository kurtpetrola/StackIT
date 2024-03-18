using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class WhiteLabelManager : MonoBehaviour
{
  private string gameSceneName = "ExampleMainMenu";

  // Input fields
  [Header("New User")]
  public TMP_InputField newUserEmailInputField;
  public TMP_InputField newUserPasswordInputField;

  [Header("Existing User")]
  public TMP_InputField existingUserEmailInputField;
  public TMP_InputField existingUserPasswordInputField;
  public CanvasAnimator loginCanvasAnimator;

  [Header("Reset password")]
  public TMP_InputField resetPasswordInputField;

  [Header("RememberMe")]
  // Components for enabling auto login
  public Toggle rememberMeToggle;
  public Animator rememberMeAnimator;
  private int rememberMe;

  [Header("Button animators")]
  public Animator autoLoginButtonAnimator;

  //start screen
  public CanvasAnimator startCanvasAnimator;
  public Animator startGuestLoginButtonAnimator;
  public Animator startNewUserButtonAnimator;
  public Animator startLoginButtonAnimator;
  public Animator startResetPasswordButtonAnimator;

  //create screen
  public Animator createButtonAnimator;
  public Animator createBackButtonAnimator;
  public Animator createPasswordInputFieldAnimator;
  public Animator createEmailInputFieldAnimator;

  //reset screen 
  public Animator resetEmailInputFieldAnimator;
  public Animator resetBackButtonAnimator;
  public Animator resetPasswordButtonAnimator;
  public CanvasAnimator resetCanvasAnimator;

  //login screen
  public Animator loginEmailInputFieldAnimator;
  public Animator loginPasswordInputFieldAnimator;
  public Animator loginBackButtonAnimator;
  public Animator loginButtonAnimator;
  public Animator loginRememberMeAnimator;

  // error screen
  public Animator errorScreenAnimator;

  //newName screen
  public Animator newNickNameInputFieldAnimator;
  public Animator newNickNameLogOutButtonAnimator;
  public Animator newNickNameCreateButtonAnimator;
  public CanvasAnimator setDisplayNameCanvasAnimator;


  [Header("New Player Name")]
  public TMP_InputField newPlayerNameInputField;

  [Header("Player name")]
  public TextMeshProUGUI playerNameText;
  public Animator playerNameTextAnimator;

  [Header("Error Handling")]
  public TextMeshProUGUI errorText;
  public GameObject errorPanel;

  public void PlayGame()
  {
    //load scene
    SceneManager.LoadScene(gameSceneName);
  }

  // Called when pressing "LOGIN" on the login-page
  public void Login()
  {
    string email = existingUserEmailInputField.text;
    string password = existingUserPasswordInputField.text;

    void isError(string error)
    {
      if (error.Contains("message"))
      {
        ShowErrorMessage(ExtractMessageFromLootLockerError(error));
      }

      if (!error.Contains("message"))
      {
        ShowErrorMessage("Error logging in");
      }

      loginButtonAnimator.SetTrigger("Error");
      loginRememberMeAnimator.SetTrigger("Show");
      loginEmailInputFieldAnimator.SetTrigger("Show");
      loginPasswordInputFieldAnimator.SetTrigger("Show");
      loginBackButtonAnimator.SetTrigger("Show");
      return;
    }

    LootLockerSDKManager.WhiteLabelLogin(email, password, Convert.ToBoolean(rememberMe), response =>
    {
      if (!response.success)
      {
        // Error
        isError(response.errorData.message);
        Debug.Log("error while logging in");
        return;
      }
      else
      {
        Debug.Log("Player was logged in succesfully");
      }

      // Is the account verified?
      if (response.VerifiedAt == null)
      {
        // Stop here if you want to require your players to verify their email before continuing
      }

      LootLockerSDKManager.StartWhiteLabelSession((response) =>
          {
            if (!response.success)
            {
              // Error
              isError(response.errorData.message);
              return;
            }
            else
            {
              PlayerPrefs.SetString("LLplayerId", response.player_id.ToString());
              // Session was succesfully started;
              // animate the buttons
              loginButtonAnimator.SetTrigger("LoggedIn");
              loginButtonAnimator.SetTrigger("Hide");
              Debug.Log("session started successfully");
              CheckIfPlayerHasName(response.public_uid);
            }
          });
    });
  }

  //checks if user has set a display name, if not forces them to set one
  public void CheckIfPlayerHasName(string publicUID)
  {
    string playerName;
    LootLockerSDKManager.GetPlayerName((response) =>
    {
      if (response.success)
      {
        playerName = response.name;
        //if the players name is the same as their publicUID, they have not set a display name
        if (playerName == "" || playerName.ToLower() == publicUID.ToLower())
        {
          // Player does not have a name, force them to set one
          Debug.Log("Player has not set a display name");
          //show the set display name screen
          setDisplayNameCanvasAnimator.CallAppearOnAllAnimators();
        }
        else
        {
          // Player has a name, continue
          Debug.Log("Player has a name: " + response.name);
          playerName = response.name;
          //load the game
          PlayGame();
        }
      }
    });

  }

  public void UpdatePlayerName()
  {
    newNickNameCreateButtonAnimator.SetTrigger("UpdateName");
    newNickNameLogOutButtonAnimator.SetTrigger("Hide");
    newNickNameInputFieldAnimator.SetTrigger("Hide");

    string newPlayerName = newPlayerNameInputField.text;
    if (newPlayerName == "")
    {
      ShowErrorMessage("Please enter a display name");
      return;
    }

    void isError(string error)
    {
      if (error.Contains("message"))
      {
        string message = ExtractMessageFromLootLockerError(error);
        if (message.Contains("UNIQUE")) ShowErrorMessage("Display name already taken");
        else
        {
          ShowErrorMessage(message);
        }
      }

      if (!error.Contains("message"))
      {
        ShowErrorMessage("Error setting display name");
      }
      newNickNameCreateButtonAnimator.ResetTrigger("UpdateName");
      newNickNameLogOutButtonAnimator.SetTrigger("Show");
      newNickNameInputFieldAnimator.SetTrigger("Show");
      newNickNameCreateButtonAnimator.SetTrigger("Error");

      return;
    }
    // Set the players name
    LootLockerSDKManager.SetPlayerName(newPlayerName, (response) =>
    {
      if (!response.success)
      {
        isError(response.errorData.message);
        return;
      }

      setDisplayNameCanvasAnimator.CallDisappearOnAllAnimators();
      newNickNameCreateButtonAnimator.SetTrigger("Hide");
      // Write the players name to the screen
      //load the game
      PlayGame();
    });
  }

  // Show an error message on the screen
  public void ShowErrorMessage(string message, int showTime = 3)
  {
    //set active
    errorPanel.SetActive(true);
    errorText.text = message.ToUpper();
    errorScreenAnimator.SetTrigger("Show");
    //wait for 3 seconds and hide the error panel
    Invoke("HideErrorMessage", showTime);
  }

  private void HideErrorMessage()
  {
    errorScreenAnimator.SetTrigger("Hide");
  }

  public void Logout()
  {
    //remove the auto remember
    PlayerPrefs.SetInt("rememberMe", 0);
    rememberMeToggle.isOn = false;
    rememberMe = 0;

    createButtonAnimator.SetTrigger("Hide");
    createButtonAnimator.ResetTrigger("CreateAccount");
    createButtonAnimator.ResetTrigger("Login");
    createButtonAnimator.ResetTrigger("ResetPassword");

    existingUserEmailInputField.text = "";
    existingUserPasswordInputField.text = "";


    //end the session
    LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();
    LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
      {
        if (!response.success)
        {
          ShowErrorMessage("Error logging out");
          return;
        }
        PlayerPrefs.DeleteKey("LLplayerId");
        Debug.Log("Logged Out");
      });

  }

  // Called when pressing "CREATE" on new user screen
  public void NewUser()
  {
    string email = newUserEmailInputField.text;
    string password = newUserPasswordInputField.text;
    // string newNickName = nickNameInputField.text;


    if (email.Length < 1 || password.Length < 1)
    {
      ShowErrorMessage("Please fill in all fields");
      return;
    }

    //if password is shorter than 8 characters display an error
    if (password.Length < 8)
    {
      ShowErrorMessage("Password must be at least 8 characters long");
      return;
    }

    void isError(string error)
    {
      if (error.Contains("message"))
      {
        ShowErrorMessage(ExtractMessageFromLootLockerError(error));
      }

      if (!error.Contains("message"))
      {
        ShowErrorMessage("Error creating account");
      }
      createButtonAnimator.SetTrigger("Error");

      createBackButtonAnimator.SetTrigger("Show");
      createPasswordInputFieldAnimator.SetTrigger("Show");
      createEmailInputFieldAnimator.SetTrigger("Show");
      return;
    }


    //if passes all above checks, create the account
    Debug.Log("Creating account");
    createButtonAnimator.SetTrigger("CreateAccount");
    createBackButtonAnimator.SetTrigger("Hide");
    createPasswordInputFieldAnimator.SetTrigger("Hide");
    createEmailInputFieldAnimator.SetTrigger("Hide");


    LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
    {
      if (!response.success)
      {
        isError(response.errorData.message);
        return;
      }
      else
      {
        // Succesful response
        // Log in player to set name
        // Login the player
        LootLockerSDKManager.WhiteLabelLogin(email, password, false, response =>
            {
              if (!response.success)
              {
                isError(response.errorData.message);
                return;
              }
              // Start session
              LootLockerSDKManager.StartWhiteLabelSession((response) =>
                  {
                    if (!response.success)
                    {
                      isError(response.errorData.message);
                      return;
                    }
                    string publicUID = response.public_uid;
                    // Set nickname to be public UID 
                    string newNickName = response.public_uid;
                    // Set new nickname for player
                    LootLockerSDKManager.SetPlayerName(newNickName, (response) =>
                        {
                          if (!response.success)
                          {
                            ShowErrorMessage("Your account was created but your display name was already taken, you'll be asked to set it when you log in.", 5);
                            // Set public UID as name if setting nickname failed
                            LootLockerSDKManager.SetPlayerName(publicUID, (response) =>
                                {
                                  if (!response.success)
                                  {
                                    ShowErrorMessage("Your account was created but your display name was already taken, you'll be asked to set it when you log in.", 5);
                                  }
                                });
                          }

                          // End this session
                          LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();
                          LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
                            {
                              if (!response.success)
                              {
                                ShowErrorMessage("Account created but error ending session");
                                return;
                              }
                              Debug.Log("Account Created");
                              createButtonAnimator.SetTrigger("AccountCreated");
                              createBackButtonAnimator.SetTrigger("Show");
                              // New user, turn off remember me
                              rememberMeToggle.isOn = false;
                            });
                        });
                  });
            });
      }
    });
  }

  // Start is called before the first frame update
  public void Start()
  {
    // See if we should log in the player automatically
    rememberMe = PlayerPrefs.GetInt("rememberMe", 0);
    if (rememberMe == 0)
    {
      rememberMeToggle.isOn = false;
    }
    else
    {
      rememberMeToggle.isOn = true;
    }
  }

  // Called when changing the value on the toggle
  public void ToggleRememberMe()
  {
    bool rememberMeBool = rememberMeToggle.isOn;
    rememberMe = Convert.ToInt32(rememberMeBool);

    // Animate button
    if (rememberMeBool == true)
    {
      rememberMeAnimator.SetTrigger("On");
    }
    else
    {
      rememberMeAnimator.SetTrigger("Off");
    }
    PlayerPrefs.SetInt("rememberMe", rememberMe);
  }

  public void AutoLogin()
  {
    // Does the user want to automatically log in?
    if (Convert.ToBoolean(rememberMe) == true)
    {
      Debug.Log("Auto login");
      // Hide the buttons on the login screen
      existingUserEmailInputField.GetComponent<Animator>().ResetTrigger("Show");
      existingUserEmailInputField.GetComponent<Animator>().SetTrigger("Hide");
      existingUserEmailInputField.GetComponent<Animator>().ResetTrigger("Show");
      existingUserPasswordInputField.GetComponent<Animator>().SetTrigger("Hide");
      loginBackButtonAnimator.ResetTrigger("Show");
      loginBackButtonAnimator.SetTrigger("Hide");

      // Start to spin the login button
      loginButtonAnimator.ResetTrigger("Hide");
      loginButtonAnimator.SetTrigger("Hide");

      LootLockerSDKManager.CheckWhiteLabelSession(response =>
      {
        if (response == false)
        {
          // Session was not valid, show error animation
          // and show back button
          loginButtonAnimator.SetTrigger("Error");
          loginBackButtonAnimator.SetTrigger("Show");

          // set the remember me bool to false here, so that the next time the player press login
          // they will get to the login screen
          rememberMeToggle.isOn = false;
        }
        else
        {
          // Session is valid, start game session
          LootLockerSDKManager.StartWhiteLabelSession((response) =>
                {
                  if (response.success)
                  {
                    PlayerPrefs.SetString("LLplayerId", response.player_id.ToString());
                    // It was succeful, log in
                    loginButtonAnimator.SetTrigger("Hide");
                    loginBackButtonAnimator.SetTrigger("Hide");
                    // Write the current players name to the screen
                    CheckIfPlayerHasName(response.public_uid);
                  }
                  else
                  {
                    // Error
                    // Animate the buttons
                    loginButtonAnimator.SetTrigger("Error");
                    loginBackButtonAnimator.SetTrigger("Show");

                    Debug.Log("error starting LootLocker session");
                    // set the remember me bool to false here, so that the next time the player press login
                    // they will get to the login screen
                    rememberMeToggle.isOn = false;

                    return;
                  }

                });

        }

      });
    }
    else if (Convert.ToBoolean(rememberMe) == false)
    {
      Debug.Log("Auto login is off");
      // Continue as usual
      loginCanvasAnimator.CallAppearOnAllAnimators();
      //   loginButtonAnimator.ResetTrigger("Show");
      loginButtonAnimator.SetTrigger("Show");
    }
  }

  public void PasswordReset()
  {
    string email = resetPasswordInputField.text;
    LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
    {
      if (!response.success)
      {
        Debug.Log("error requesting password reset");
        //get the message from the error and dsiplay it 

        if (response.errorData.message.Contains("message"))
        {
          ShowErrorMessage(ExtractMessageFromLootLockerError(response.errorData.message));
        }

        if (!response.errorData.message.Contains("message"))
        {
          ShowErrorMessage("Error requesting password reset");
        }

        resetPasswordButtonAnimator.SetTrigger("Error");

        // make the buttons show again 
        resetBackButtonAnimator.SetTrigger("Show");
        resetEmailInputFieldAnimator.SetTrigger("Show");

        return;
      }

      Debug.Log("requested password reset successfully");
      resetEmailInputFieldAnimator.SetTrigger("Hide");
      resetPasswordButtonAnimator.SetTrigger("Done");
      resetBackButtonAnimator.SetTrigger("Show");
    });
  }

  public void ResendVerificationEmail()
  {
    int playerID = 0;
    LootLockerSDKManager.WhiteLabelRequestVerification(playerID, (response) =>
    {
      if (response.success)
      {
        // Email was sent!
      }
    });
  }

  public void GuestLogin()
  {
    //made guest login spin to show loading
    startGuestLoginButtonAnimator.SetTrigger("Login");
    //hide all other buttons
    startCanvasAnimator.CallDisappearOnAllAnimators(startGuestLoginButtonAnimator.name);


    Debug.Log("Guest login");

    //if theres a player identifier saved in browser, log the user in with that, if not create a new guest session

    string guestId = PlayerPrefs.GetString("LLguestId", "Nada");

    if (guestId == "Nada")
    {
      LootLockerSDKManager.StartGuestSession((response) =>
          {
            if (!response.success)
            {
              Debug.Log("error starting LootLocker session");
              ShowErrorMessage("Error logging in as a guest");
              startGuestLoginButtonAnimator.SetTrigger("Error");

              startCanvasAnimator.CallAppearOnAllAnimators();
              return;
            }
            PlayerPrefs.SetString("LLplayerId", response.player_id.ToString());

            startCanvasAnimator.CallDisappearOnAllAnimators();
            // Load game screen
            Debug.Log(response.public_uid);
            CheckIfPlayerHasName(response.public_uid);
            //save identifier to player prefs
            PlayerPrefs.SetString("LLguestId", response.player_identifier);
            Debug.Log("successfully started LootLocker session");
          });
    }

    if (guestId != "Nada")
    {
      LootLockerSDKManager.StartGuestSession(guestId, (response) =>
          {
            if (!response.success)
            {
              Debug.Log("error starting LootLocker session");
              ShowErrorMessage("Error logging in as a guest");
              startGuestLoginButtonAnimator.SetTrigger("Error");

              startCanvasAnimator.CallAppearOnAllAnimators();
              return;
            }
            PlayerPrefs.SetString("LLplayerId", response.player_id.ToString());
            startCanvasAnimator.CallDisappearOnAllAnimators();
            // Load game screen
            Debug.Log(response.public_uid);
            CheckIfPlayerHasName(response.public_uid);
            //save identifier to player prefs
            PlayerPrefs.SetString("LLguestId", response.player_identifier);
            Debug.Log("successfully started LootLocker session");
          });
    }


  }

  private string ExtractMessageFromLootLockerError(string rawError)
  {
    //find in the string "message":" and split the string there
    int first = rawError.IndexOf("\"message\":\"") + "\"message\":\"".Length;
    int last = rawError.LastIndexOf("\"message\":\"");
    // removes "message":" and everything before it from the string
    string str2 = rawError.Substring(first, rawError.Length - first);

    int end = str2.IndexOf("\"");
    // finds the closing " and removes everything after it from the string 
    string res = str2.Substring(0, end);
    res = res.ToUpper();
    return res;
  }
}
