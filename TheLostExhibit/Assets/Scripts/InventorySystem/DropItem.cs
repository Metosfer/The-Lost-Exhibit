using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public InventoryController inventory;

    void Update()
    {

        if (inventory.isFull[0] && Input.GetKeyDown(KeyCode.Alpha1)) DropObj(inventory.slots[0]);
        if (inventory.isFull[1] && Input.GetKeyDown(KeyCode.Alpha2)) DropObj(inventory.slots[1]);
        if (inventory.isFull[2] && Input.GetKeyDown(KeyCode.Alpha3)) DropObj(inventory.slots[2]);
    }

    public void DropObj(GameObject dropSlot)
    {
        if (dropSlot.GetComponent<Slot>().amount > 1)
        {
            dropSlot.GetComponent<Slot>().amount -= 1;
        }
        else if (dropSlot.GetComponent<Slot>().amount == 1)
        {
            dropSlot.GetComponent<Slot>().amount -= 1;
            GameObject.Destroy(dropSlot.transform.GetComponentInChildren<SpawnItems>().gameObject);
        }

        dropSlot.transform.GetComponentInChildren<SpawnItems>().SpawnDroppedItem();
    }
}
