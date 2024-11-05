using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public InventoryController inventory;

    public GameObject craftObj_Button;
    public string itemName;


    public void Craft()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == true && inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().itemName == itemName && inventory.slots[i].transform.GetComponentInChildren<Slot>().amount == 2)
            {
                Destroy(inventory.slots[i].transform.GetComponentInChildren<SpawnItems>().gameObject);
                Instantiate(craftObj_Button, inventory.slots[i].transform, false);
                inventory.slots[i].GetComponent<Slot>().amount = 1;
                break;
            }
        }
    }
}
