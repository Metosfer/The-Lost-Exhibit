using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; set; }

    public bool[] isFull;
    public GameObject[] slots;
    public int stackLimit;
    public GameObject hint;

    public GameObject inventoryPanel;
    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (!(isFull[0] || isFull[1] || isFull[2])) inventoryPanel.SetActive(false);
        else inventoryPanel.SetActive(true);
    }
}
