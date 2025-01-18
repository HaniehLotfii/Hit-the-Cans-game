using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundFXSource;
    public AudioClip ballThrowSound;
    public AudioClip gameOverSound;

    public AudioClip ballHitCanSound;

    public AudioClip popSound; 


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        soundFXSource.PlayOneShot(clip);
    }
}
