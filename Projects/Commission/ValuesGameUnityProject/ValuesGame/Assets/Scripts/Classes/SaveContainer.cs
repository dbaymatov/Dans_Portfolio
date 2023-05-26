using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveContainer
{
    public int gameStage;
    public int character;
    public List<ValueClass> saveList1;
    public List<ValueClass> saveList2;
    public List<ValueClass> saveList3;


    public SaveContainer()
    {
        saveList1 = new List<ValueClass>();
        saveList2 = new List<ValueClass>();
        saveList3= new List<ValueClass>();
        gameStage = 1;
        character = 1;
    }
}
