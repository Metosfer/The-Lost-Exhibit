using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public int stackLimit;
    public GameObject hint;

    public GameObject inventoryPanel;

    private void Update()
    {
        if (!(isFull[0] || isFull[1] || isFull[2])) inventoryPanel.SetActive(false);
        else inventoryPanel.SetActive(true);
    }
}
