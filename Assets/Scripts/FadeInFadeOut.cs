using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInFadeOut : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Call the FadeToLevel function with the index of the current scene as the destination.
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    // Call this function to initiate the fade-out animation and transition to the specified level.
    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    // This function will be called by the Animation Event in your fade-out animation.
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
