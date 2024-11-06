using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 2.4f, -4.7f);
    void Start()
    {
        
    }

  
    void LateUpdate()
    {
        transform.position = playerPrefab.transform.position + offset;
    }
}
