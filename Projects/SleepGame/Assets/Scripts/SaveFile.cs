using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public string username;
    public bool tutorial;
    public bool allIslands;
    public bool results;
    public List<IslandDataContainer> islandDataList;
    public List<InventoryItem> chosenAdviceList;

    public void Initialise()
    {
        tutorial = false;
        allIslands = false;
        results = false;
        username = "";
        chosenAdviceList = new List<InventoryItem>();
        islandDataList = new List<IslandDataContainer>(new IslandDataContainer[6]);//the number will correspond to the amount of islands scenes present + resultscreen chosen items, and each will access its corresponding data container
        for (int i = 0; i < islandDataList.Count;)
        {
            islandDataList[i] = new IslandDataContainer();
            i++;
        }
    }

}
