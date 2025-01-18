using UnityEngine;

public class Rock : MonoBehaviour
{
    
    public bool isStable = true; // Taþýn saðlam olup olmadýðýný kontrol eder
    public float destroyDuration = 0.3f; // Taþýn yok olma süresi



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

        
        Destroy(gameObject, destroyDuration); // 3 saniye sonra taþý yok et
        
    }
}
