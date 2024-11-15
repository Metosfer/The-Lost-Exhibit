using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Singleton instance
    private static SceneManager instance;
    
    public static SceneManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag and this object has "Caravan" tag
        if (other.CompareTag("Player") && gameObject.CompareTag("Caravan"))
        {
            // Load scene with index 1
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

}