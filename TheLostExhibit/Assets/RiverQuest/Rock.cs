using UnityEngine;

public class Rock : MonoBehaviour
{
    
    public bool isStable = true; // Ta��n sa�lam olup olmad���n� kontrol eder
    public float destroyDuration = 0.3f; // Ta��n yok olma s�resi



    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isStable)
        {
            Fall();
            
        }
    }

    private void Fall()
    {

        
        Destroy(gameObject, destroyDuration); // 3 saniye sonra ta�� yok et
        
    }
}
