using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RecuiredAmount { get; set; }

    public InventoryController inventory { get; set; }


    void Start()
    {
       inventory = FindObjectOfType<InventoryController>();

    }

    public virtual void Init()
    {
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RecuiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Completed = true;
        this.Quest.CheckGoals();
        Completed = true;
        Debug.Log("Goal marked as completed.");
    }
}
