using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    private bool hasCollectedTicket = false;
    private InventoryController inventory;

    void Start()
    {
        if (tutorialText == null)
        {
            tutorialText = GetComponent<TextMeshProUGUI>();
        }

        inventory = FindObjectOfType<InventoryController>();
        UpdateTutorialText();
    }

    public void OnTicketCollected()
    {
        hasCollectedTicket = true;
        UpdateTutorialText();
    }

    private void UpdateTutorialText()
    {
        if (!hasCollectedTicket)
        {
            tutorialText.text = "Press E to take your ticket";
        }
        else
        {
            tutorialText.text = "You can move with W A S D keys and look around with mouse";
        }
    }
}