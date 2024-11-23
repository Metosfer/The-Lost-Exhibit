using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    private InventoryController inventory;
    public GameObject itemButton;
    public string itemName;
    private PlayerAnimation playerAnimation;
    private TutorialManager tutorialManager;

    void Start()
    {
        inventory = FindObjectOfType<InventoryController>();

        playerAnimation = FindObjectOfType<PlayerAnimation>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
                PickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.hint.SetActive(false);
        }
    }

    public void PickUp()
    {

        playerAnimation?.SetPickUpState(true);

        // Eðer bu bilet ise tutorial metnini güncelle
        if (itemName == "Ticket" && tutorialManager != null)
        {
            tutorialManager.OnTicketCollected();
        }

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == true && inventory.slots[i].transform.GetComponent<Slot>().amount < inventory.stackLimit)
            {
                if (itemName == inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName) //stack
                {
                    Destroy(gameObject);
                    inventory.slots[i].GetComponent<Slot>().amount += 1;
                    break;
                }
            }
            else if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                inventory.slots[i].GetComponent<Slot>().amount += 1;
                Destroy(gameObject);
                break;
            }
        }
        inventory.GetComponentInChildren<Crafting>().Craft();
        checkQuest();

    }

    public void checkQuest()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == true)
            {
                if (inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName == "Hoop_quest_1")
                {
                    itemName = inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName;
                    InventoryEvents.PickUpItems(this);
                }

                if (inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName == "Rope_quest_1")
                {
                    itemName = inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName;
                    InventoryEvents.PickUpItems(this);
                }

                if (inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName == "Feather_quest_1")
                {
                    itemName = inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName;
                    InventoryEvents.PickUpItems(this);
                }
            }
        }
    }
}