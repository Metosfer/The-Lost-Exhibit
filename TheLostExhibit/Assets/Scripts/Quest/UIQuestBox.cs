using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIQuestBox : MonoBehaviour
{

    public GameObject questPanel;

    public GameObject QuestDescription;
    public GameObject QuestName;

    public GameObject QuestGiver;


    // Start is called before the first frame update
    void Start()
    {
        questPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questPanel.activeSelf) questPanel.SetActive(false);
            else
            {
                if (this.GetComponents<GiftForTheNewborn>().Length == 1) FillQuestPanel();
                questPanel.SetActive(true);
            }
        }
    }

    void FillQuestPanel()
    {
        if (this.GetComponent<GiftForTheNewborn>().Completed == false)
        {
            QuestName.GetComponent<TextMeshProUGUI>().text = this.GetComponent<GiftForTheNewborn>().QuestName;
            QuestDescription.GetComponent<TextMeshProUGUI>().text = this.GetComponent<GiftForTheNewborn>().Description;

            foreach (var desGoal in this.GetComponent<GiftForTheNewborn>().Goals)
            {
                QuestDescription.GetComponent<TextMeshProUGUI>().text += "\n\n" + desGoal.Description;
            }

        }
        else if (QuestGiver.GetComponent<QuestGiver>().Helped && QuestGiver.GetComponent<QuestGiver>().AssignedQuest == false) 
        {
            QuestName.GetComponent<TextMeshProUGUI>().text = "Quest";
            QuestDescription.GetComponent<TextMeshProUGUI>().text = "You don't have a quest right now";
        }
        
    }
}
