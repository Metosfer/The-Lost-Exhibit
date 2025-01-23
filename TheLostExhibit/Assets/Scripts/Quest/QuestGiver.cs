using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    public GameObject clue;
    public Mesh hasQuest;
    public Mesh waitQuest;

    [SerializeField]
    private GameObject quests;

    [SerializeField]
    private string questType;

    private Quest Quest { get; set; }

    private void Update()
    {
        if (!AssignedQuest && !Helped) clue.GetComponent<MeshFilter>().mesh = hasQuest;
        if (AssignedQuest && !Helped) clue.GetComponent<MeshFilter>().mesh = waitQuest;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interacting with NPC...");
                Interact();
            }
        }
    }

    public void Interact()
    {
        if (!AssignedQuest && !Helped)
        {
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else if (Helped)
        {
            // Automatically load the InBetweenScene if the quest is completed
            Debug.Log("Quest completed! Loading InBetweenScene...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("InBetweenScene");
        }
    }

    private void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        Debug.Log("Quest assigned: " + questType);
    }

    private void CheckQuest()
    {
        if (Quest != null && Quest.Completed)
        {
            Debug.Log("Quest completed! Returning to NPC...");
            clue.SetActive(false); // Hide the quest clue
            Helped = true;
            AssignedQuest = false;
        }
        else
        {
            Debug.Log("Quest not yet complete. Keep working!");
        }
    }
}

