using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject AdjustMusic;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Start playing music if it's not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Method to stop music
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Method to pause music
    public void PauseMusic()
    {
        audioSource.Pause();
    }

    // Method to resume music
    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

    // Method to change the music clip dynamically (if needed)
    public void ChangeMusic(AudioClip newMusic)
    {
        audioSource.clip = newMusic;
        audioSource.Play();
    }
    public void B_Music()
    {
        AdjustMusic.SetActive(true);
    }
    public void B_Close()
    {
        AdjustMusic.SetActive(false);
    }
}
