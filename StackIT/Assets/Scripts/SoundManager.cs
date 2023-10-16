using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnicon;
    [SerializeField] Image soundOfficon;
    private bool muted = false;
    private AudioSource soundEffectSource; // Reference to the AudioSource component for sound effects

    public AudioClip yourSoundEffect; // Reference to your sound effect AudioClip

    void Start()
    {
        // Initialize the soundEffectSource reference
        soundEffectSource = gameObject.AddComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }

    public void PlayYourSoundEffect()
    {
        // Check if the sound is not muted
        if (!muted && yourSoundEffect != null)
        {
            soundEffectSource.PlayOneShot(yourSoundEffect);
        }
    }

    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnicon.enabled = true;
            soundOfficon.enabled = false;
        }
        else
        {
            soundOnicon.enabled = false;
            soundOfficon.enabled = true;
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
