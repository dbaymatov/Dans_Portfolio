using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds all the item data that will be referenced whever that object is selected; texture, choice, replies.
/// </summary>

public class ItemLibrary : MonoBehaviour
{

    [SerializeField] List<InventoryItem> items;

    public InventoryItem FindItemByID(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
                return items[i];
        }

        return items[0];
    } 

}
