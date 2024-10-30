using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public string itemName;
    public GameObject itemPrefab;
    public float offset = 1;
    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Vector3 playePos = new Vector3(player.position.x + Random.Range(0, offset), player.position.y + 1, player.position.z + offset);
        Instantiate(itemPrefab, playePos, Quaternion.identity); 
    }
}
