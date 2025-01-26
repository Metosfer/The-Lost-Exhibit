using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyQMSC : MonoBehaviour
{
    public bool playerCollided = false; 
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            playerCollided = true;
            Destroy(gameObject);
        }
    }
}
