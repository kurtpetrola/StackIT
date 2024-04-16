using UnityEngine;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    public GameObject helpCanvas; // Reference to the help Canvas GameObject.

    // Function to toggle the help Canvas on/off.
    public void ToggleHelp()
    {
        helpCanvas.SetActive(!helpCanvas.activeSelf);
    }
}
