using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents : MonoBehaviour
{
    public delegate void PickUpEventHandler(PickUpItems item);
    public static event PickUpEventHandler ItemPickedUp;

    public static void PickUpItems(PickUpItems someItem)
    {
        if (ItemPickedUp != null)
            ItemPickedUp(someItem);
    }
}
