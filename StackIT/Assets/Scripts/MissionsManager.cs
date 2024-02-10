using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public GameObject missionsCanvas; // Reference to the missions Canvas GameObject.

    // Function to toggle the settings Canvas on/off.
    public void ToggleMissions()
    {
        missionsCanvas.SetActive(!missionsCanvas.activeSelf);
    }
}
