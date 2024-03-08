using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    public GameObject panelToShow; // Assign your panel GameObject in the Unity Editor

    // Update is called once per frame
    void Update()
    {
        // Check if the back button is pressed on Android devices
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Show the panel if it's not already active
            if (panelToShow != null && !panelToShow.activeSelf)
            {
                panelToShow.SetActive(true);
            }
        }
    }
}
