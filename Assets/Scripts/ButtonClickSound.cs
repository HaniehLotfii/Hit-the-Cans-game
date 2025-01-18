using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public Button button;
    public AudioClip popSound; 

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(PlayPopSound);
        }
    }

    void PlayPopSound()
    {
        SoundManager.instance.PlaySound(popSound);
    }
}
