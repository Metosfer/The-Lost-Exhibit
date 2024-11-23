using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftForTheNewborn : Quest
{
    void Start()
    {
        Debug.Log("GiftForTheNewborn assigned.");

        QuestName = "Gift For The Newborn";
        Description = "Gather materials to help a mother make a dreamcatcher for her newborn";

        Goals = new List<Goal>
        {
            new CollectionGoal (this, "Hoop_quest_1", "Collect Willow Hoop. To do this, you will need 3 Willow Branches. Follow a trail leading to a Willow Tree by the water.", false, 0, 1),
            new CollectionGoal (this, "Rope_quest_1", "Collect Sinew Thread. Go to the leatherworker’s tent.", false, 0, 2),
            new CollectionGoal (this, "Feather_quest_1", "Collect Feathers. Head into the forest.", false, 0, 4)

        };

        Goals.ForEach(g => g.Init());
    }
}
