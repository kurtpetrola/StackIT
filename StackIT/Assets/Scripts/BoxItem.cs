using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
    [SerializeField] public Text toastText; // Assign this in the Unity Editor
    private bool isToastVisible = false;

    private void Start()
    {
        if (toastText == null)
        {
            Debug.LogWarning("Toast Text object not assigned in the inspector.");
        }
        else
        {
            toastText.enabled = false; // Hide the text initially
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Box"))
        {
            ShowToastAndHideAfterDelay("You clicked the Box!", 1f);
        }
        else if (gameObject.CompareTag("Platform"))
        {
            ShowToastAndHideAfterDelay("You clicked the Platform!", 0.5f);
        }
    }

    private void ShowToastAndHideAfterDelay(string message, float delay)
    {
        if (toastText != null)
        {
            isToastVisible = true;
            toastText.text = message;
            toastText.enabled = isToastVisible; // Show the text
            StartCoroutine(HideToastAfterDelay(delay)); // Hide the message after the specified delay
        }
        else
        {
            Debug.LogWarning("Toast Text object not found or assigned.");
        }
    }

    IEnumerator HideToastAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isToastVisible = false;
        if (toastText != null)
        {
            toastText.enabled = isToastVisible; // Hide the text after delay
        }
    }
}