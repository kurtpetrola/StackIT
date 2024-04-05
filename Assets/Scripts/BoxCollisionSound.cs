using UnityEngine;

public class BoxCollisionSound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false; // This flag will check if the sound has been played

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the sound has already been played
        if (!hasPlayed && collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Platform"))
        {
            // Play the sound effect
            audioSource.Play();

            // Set the flag to true to indicate the sound has been played
            hasPlayed = true;
        }
    }
}
