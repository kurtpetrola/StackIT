using UnityEngine;
using UnityEngine.UI;

public class LifeUIManager1 : MonoBehaviour
{
    public Image[] heartIcons; // Assign the heart images to this array in the inspector

    private void Start()
    {
        UpdateHeartIcons();
    }

    public void UpdateHeartIcons()
    {
        int currentLives = LifeManager1.Instance.lives;

        // You may want to validate that the heartIcons array has the correct length
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < currentLives)
            {
                heartIcons[i].gameObject.SetActive(true);
            }
            else
            {
                heartIcons[i].gameObject.SetActive(false);
            }
        }
    }
}