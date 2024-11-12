using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private InventoryController inventory;
    public int i;
    public TextMeshProUGUI amountText;
    public int amount;

    void Start()
    {
        inventory = FindObjectOfType<InventoryController>();
    }


    void Update()
    {
        amountText.text = amount.ToString();

        if (amount > 0)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else if (amount <= 0) transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;

        if(transform.childCount == 2) // 2 - count of defolt child - means that slot is empty
        {
            inventory.isFull[i] = false;
        }

    }


    public void DropItem()
    {
        if (amount > 1)
        {
            amount -= 1;
        }
        else if (amount == 1)
        {
            amount -= 1;
            GameObject.Destroy(transform.GetComponentInChildren<SpawnItems>().gameObject);
        }

        transform.GetComponentInChildren<SpawnItems>().SpawnDroppedItem();
    }
}
