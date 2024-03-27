using System.Collections;
using UnityEngine;

public class NetworkCheck : MonoBehaviour
{
    public GameObject noConnectionPanel;
    public StartPanel startPanelScript;

    private bool previouslyNotReachable = false; // To track previous connection status

    private void Start()
    {
        startPanelScript = FindObjectOfType<StartPanel>(); // Find the StartPanel script in the scene
        StartCoroutine(CheckInternetConnection());
    }

    IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            bool isNotReachable = Application.internetReachability == NetworkReachability.NotReachable;
            
            if (isNotReachable && !previouslyNotReachable)
            {
                Time.timeScale = 0;
                Debug.Log("No Internet Connection! Showing Panel.");
                noConnectionPanel.SetActive(true);
                startPanelScript.OnNetworkDisconnected();
                previouslyNotReachable = true;
            }
            else if (!isNotReachable && previouslyNotReachable)
            {
                Debug.Log("Internet Connection Detected. Hiding Panel.");
                noConnectionPanel.SetActive(false);
                startPanelScript.OnNetworkReconnected();
                previouslyNotReachable = false;
            }

            yield return new WaitForSeconds(3f); // Check every 3 seconds
        }
    }
}