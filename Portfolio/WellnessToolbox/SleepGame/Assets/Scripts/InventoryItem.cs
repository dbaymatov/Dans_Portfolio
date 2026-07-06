using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// data container for each individual inventory item
/// </summary>
[System.Serializable]
public class InventoryItem
{
    public int id;
    public string itemName;
    public string question;
    public List<string> questionAnsweres;

    [System.Xml.Serialization.XmlIgnoreAttribute] 
    public Sprite itemTxt;
    public string goodAnswer;
    public string badAnswer;
    public string shortDesc;
    public bool active;
    public bool answeredCorrectly;

    public InventoryItem()
    {
         id=0;
         itemName="empty";
         question="empyt";
         questionAnsweres= new List<string>();
        itemTxt = null ;
         goodAnswer="empty";
         badAnswer= "empty";
         shortDesc = "empty";
         active  = true;
         answeredCorrectly = false;
    }

}
