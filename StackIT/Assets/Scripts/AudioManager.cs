using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Button muteButton;    // Reference to the "Mute" button.
    public Button unmuteButton;  // Reference to the "Unmute" button.

    private bool isMuted = false;

    public static AudioManager instance;

    void Awake()
    {
        // Singleton pattern for AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme-Music");
        UpdateButtonVisibility();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        // Toggle button visibility
        muteButton.gameObject.SetActive(isMuted);
        unmuteButton.gameObject.SetActive(!isMuted);
        Debug.Log("Mute State: " + isMuted);
    }

    private void UpdateButtonVisibility()
    {
        // Set initial button visibility based on the mute state
        muteButton.gameObject.SetActive(!isMuted);
        unmuteButton.gameObject.SetActive(isMuted);
    }
}
