using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Transform respawnPoint;
    public AudioSource fallSoundFX;




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fallSoundFX.Play();
            other.transform.position = respawnPoint.position;
        }
    }

}
