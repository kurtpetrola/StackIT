using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public string sceneName;
    // Start is called before the first frame update
    public void changeScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
        // UnloadRecentGameScene();
        // SceneManager.LoadScene("MainScene");
    }
}
