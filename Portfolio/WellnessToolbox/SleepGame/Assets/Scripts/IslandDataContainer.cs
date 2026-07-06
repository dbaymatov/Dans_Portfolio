using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IslandDataContainer
{
    public bool complete;
    public int islandCount;
    public List<InventoryItem> sceneItems;

    public IslandDataContainer(List<InventoryItem> items)//the constructor takes in the list of the items in the scene
    {
        sceneItems = items;
    }
    public IslandDataContainer(bool finish)
    {
        complete = finish;
    }
    public IslandDataContainer()//no argument constructor
    {
        sceneItems = new List<InventoryItem>();
        complete = false;
        islandCount = 0;
    }

}

