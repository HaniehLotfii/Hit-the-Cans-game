using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
        Vector3 spwanPos;

        void Start()
        {
            spwanPos = transform.position;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Resetter"))
            {
                RepositionBall();
            }
        }

        public void RepositionBall()
        {
            this.gameObject.SetActive(false);
            transform.position = spwanPos;
            this.GetComponent<Animator>().enabled = true;
            gameObject.SetActive(true);
        }

}
