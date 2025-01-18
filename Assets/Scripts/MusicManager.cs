using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject AdjustMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

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
