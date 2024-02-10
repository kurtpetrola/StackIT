using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public GameObject creditsCanvas; // Reference to the creditss Canvas GameObject.

    // Function to toggle the credits Canvas on/off.
    public void ToggleCredits()
    {
        creditsCanvas.SetActive(!creditsCanvas.activeSelf);
    }
}
