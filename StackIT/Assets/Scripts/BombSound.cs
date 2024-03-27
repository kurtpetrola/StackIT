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
}
