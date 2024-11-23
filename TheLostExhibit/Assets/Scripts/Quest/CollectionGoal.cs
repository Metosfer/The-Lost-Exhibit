using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class CollectionGoal : Goal
{
    public string ItemName { get; set; }

    public CollectionGoal(Quest quest, string itemName, string description, bool completed, int currentAmount, int recuiredAmount)
    {
        this.Quest = quest;
        this.ItemName = itemName;
        this.Description = description; 
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RecuiredAmount = recuiredAmount;
    }

    public override void Init()
    {
        base.Init();
        InventoryEvents.ItemPickedUp += ItemPickedUp;
    }

    public void ItemPickedUp(PickUpItems pickUp)
    {
        Debug.Log("Detected ItemPickedUp");

        if (pickUp.itemName == this.ItemName)
        {
            this.CurrentAmount++;
            Evaluate();
        }

    }
}
