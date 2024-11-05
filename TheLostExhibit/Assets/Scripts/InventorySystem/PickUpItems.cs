using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    private InventoryController inventory;
    public GameObject itemButton;
    public string itemName;



    void Start()
    {
        inventory = FindObjectOfType<InventoryController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E)) PickUp();
        }
    }

    public void PickUp()
    {
        Debug.Log("on PickUp");
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
