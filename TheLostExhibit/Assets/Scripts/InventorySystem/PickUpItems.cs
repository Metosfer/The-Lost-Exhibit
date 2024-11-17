using System.Collections;
using System.Collections.Generic;
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
            inventory.hint.GetComponentInChildren<TextMeshProUGUI>().text = "Press E";
            inventory.hint.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
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
        Debug.Log("on PickUp");
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
    }
}