using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public void Update()
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
                Debug.Log("Interact");
                Interact();
            }
        }
    }
    public  void Interact()
    {

        if (!AssignedQuest && !Helped)
        {
            AssignQuest();
        }
        else if (AssignedQuest && !Helped)
        {
            CheckQuest();
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {

        Debug.Log(Quest.Completed);
        if (Quest.Completed)
        {
            clue.SetActive(false);
            Debug.Log("Complete quest" + "  " + Quest.Description);
            //можно добавить награду или что либо еще
            Helped = true;
            AssignedQuest = false;
        }
        else
        {
            Debug.Log("Not yet complete");
            //символ вопроса над нпц
            // что-то что скажет что гг еще помогает, нпц ожидает помощи
        }
    }

}
