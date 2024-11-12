using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    private InventoryController inventory;
    public string itemName;
    public GameObject itemPrefab;
    private Transform player;

    float offset = 1;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Vector3 playePos = new Vector3(player.position.x + Random.Range(0, offset), player.position.y + 1, player.position.z + Random.Range(0, offset));
        Instantiate(itemPrefab, playePos, Quaternion.identity); 
    }
}
