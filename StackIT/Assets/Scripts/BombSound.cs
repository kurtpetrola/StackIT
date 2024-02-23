using UnityEngine;

public class BombSound : MonoBehaviour
{
    public AudioSource droppingSound;
    public AudioSource impactSound;
    private bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the sounds are not playing at start
        droppingSound.Stop();
        impactSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the GameObject is falling
        if (!isFalling && transform.position.y < 4 && GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            isFalling = true;
            droppingSound.Play();
        }
    }

    // This function is called when the Rigidbody collides with another Rigidbody
    private void OnCollisionEnter2d(Collision2D collision)
    {
        // Check if the GameObject has collided with the platform
        if (isFalling && collision.gameObject.CompareTag("Platform"))
        {
            // Stop the dropping sound and play the impact sound
            droppingSound.Stop();
            impactSound.Play();
            isFalling = false;
        }
    }
}
