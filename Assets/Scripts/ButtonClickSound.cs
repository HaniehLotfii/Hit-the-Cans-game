using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public Button button; // The button to which this script is attached
    public AudioClip popSound; // The pop sound effect

    private void Start()
    {
        // Ensure the button is linked to the script
        if (button != null)
        {
            button.onClick.AddListener(PlayPopSound); // Add listener for button click
        }
    }

    void PlayPopSound()
    {
        // Play the pop sound effect
        SoundManager.instance.PlaySound(popSound);
    }
}
