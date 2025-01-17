using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public bool hasFallen;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Resetter"))
        {
            hasFallen = true;
            GameManager.instance.GroundFallenCheck();
            UIManager.instance.UpdateScore();

            // SoundManager.instance.PlaySound(SoundManager.instance.canFallSound);
        }
    }
    void OnCollisionEnter(Collision collision)
{
    // Check if the object colliding is the ball
    if (collision.gameObject.CompareTag("Ball"))
    {
        // Play the sound effect when the ball hits the can
        SoundManager.instance.PlaySound(SoundManager.instance.ballHitCanSound);
    }

    // If the can is reset (optional), you can check for a different trigger, if needed.
    if (collision.gameObject.CompareTag("Resetter"))
    {
        hasFallen = true;
        GameManager.instance.GroundFallenCheck();
        UIManager.instance.UpdateScore();
        
        // Optionally, you can add sound when the can falls
        // SoundManager.instance.PlaySound(SoundManager.instance.canFallSound);
    }
}


}
