using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftForTheNewborn : Quest
{
    void Start()
    {
        Debug.Log("GiftForTheNewborn assigned.");

        QuestName = "GIFT FOR THE NEWBORN";
        Description = "Gather materials to make a dreamcatcher for newborn baby";

        Goals = new List<Goal>
        {
            new CollectionGoal (this, "Hoop_quest_1", "CRAFT A HOOP. To do this, find 3 Branches.", false, 0, 1),
            new CollectionGoal (this, "Rope_quest_1", "COLLECT 2 SINEW THREAD. Look aroound the village.", false, 0, 2),
            new CollectionGoal (this, "Feather_quest_1", "COLLECT 4 FEATHERS. Head into the forest.", false, 0, 4)

        };

        Goals.ForEach(g => g.Init());
    }
}
