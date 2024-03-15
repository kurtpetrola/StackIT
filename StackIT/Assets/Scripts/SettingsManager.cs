using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsCanvas; // Reference to the settings Canvas GameObject.

    // Function to toggle the settings Canvas on/off.
    public void ToggleSettings()
    {
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
    }
}
