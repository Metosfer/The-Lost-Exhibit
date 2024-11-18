using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private TutorialManager tutorialManager;

    void Start()
    {
        // Sahnedeki TutorialManager'ý bul
        tutorialManager = FindObjectOfType<TutorialManager>();

        if (tutorialManager == null)
        {
            Debug.LogError("TutorialManager bulunamadý!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door area");
            tutorialManager.SetPlayerNearDoor(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited door area");
            tutorialManager.SetPlayerNearDoor(false);
        }
    }
}