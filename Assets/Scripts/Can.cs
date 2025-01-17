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
        }
    }

}
