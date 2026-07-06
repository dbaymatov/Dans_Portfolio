using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// one of the dirtiest codes i ever wrote and somehow works.
/// 
/// </summary>
public class Inventory : MonoBehaviour
{
    InventoryItem selectedItem;
    List<int> inventoryItemsSlotID;
    SaveFile saveContainer;
    List<bool> activeObjects;
    List<string> advice;
    List<string> shortDesc;
    bool islandFinished = false;

    [SerializeField] int sceneID;
    [SerializeField] int totalScenes;

    [SerializeField] ItemLibrary itemLibrary;
    [SerializeField] List<GameObject> inventorySlots;

    [SerializeField] GameObject questionPanel;

    [SerializeField] TextMeshProUGUI buttonText1;
    [SerializeField] TextMeshProUGUI buttonText2;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject closeButton;

    [SerializeField] GameObject answer1;
    [SerializeField] GameObject answer2;
    [SerializeField] string wellcomeMsg;
    [SerializeField] List<GameObject> collectables;

    [SerializeField] GameObject endLevelMsg;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        IslandWellcome(wellcomeMsg);
      //  LoadData();
        UpdateInventory();
    }

    private void Update()
    {

        islandFinished = true;

        for (int i = 0; i < collectables.Count;)
        {

            if (collectables[i].activeSelf == true)
            {
                islandFinished = false;
                Debug.Log("obj not clicked ID is " + collectables[i].name);

            }

            i++;
        }

        if (islandFinished)
        {
            endLevelMsg.SetActive(true);
        }
    }

    void Initialise()
    {
        advice = new List<string>();
        shortDesc = new List<string>();
        inventoryItemsSlotID = new List<int>();
        activeObjects = new List<bool>();
        for (int i = 0; i < collectables.Count; i++)
        {
            activeObjects.Add(true);
        }
    }

    //collects data from variables and stores them in savefile class
    public void SaveData()
    {
        /*
        saveContainer.islandDataList[sceneID].activeObjectsList = activeObjects;
        saveContainer.islandDataList[sceneID].inventoryItems = inventoryItemsSlotID;
        saveContainer.islandDataList[sceneID].advice = advice;
        saveContainer.islandDataList[sceneID].shordDesc = shortDesc;
        saveContainer.islandDataList[sceneID].islandCompleted = islandFinished;
        */
        Stream stream = File.Open("SaveFiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
        serializer.Serialize(stream, saveContainer);
        stream.Close();
    }
    //contains forloop that updates all inventory slots based on the item id and sets there texture represnting the item
    void UpdateInventory()
    {
        for (int i = 0; i < inventoryItemsSlotID.Count;)
        {
            Image img = inventorySlots[i].GetComponent<Image>();
            InventoryItem item = itemLibrary.FindItemByID(i);
            img.enabled = true;
            img.sprite = item.itemTxt;
            i++;
        }



        SaveData();

    }

    //adds item to then end of inventory list based on item id
    public void addItem(int id)
    {
        Debug.Log(id);
        Debug.Log(activeObjects.Count);
        for (int i = 0; i < activeObjects.Count;)
        {
            Debug.Log(activeObjects[i]);
            i++;
        }

        Debug.Log(activeObjects);
        activeObjects[id] = false;//sets object as innactive in savefile
       // saveContainer.islandDataList[sceneID].activeObjectsList = activeObjects;
        inventoryItemsSlotID.Add(id);
        selectedItem = itemLibrary.FindItemByID(id);

        UpdateInventory();
        AskQuestion();
    }

    //disables passed in object
    public void disableObject(GameObject button)
    {
        button.SetActive(false);
    }

    //enables question panels, sets text for  question and choises 
    public void AskQuestion()
    {
        questionPanel.SetActive(true);
        questionText.text = selectedItem.question;
        buttonText1.text = selectedItem.questionAnsweres[0];
        buttonText2.text = selectedItem.questionAnsweres[1];
    }

    //based on the button clicked gives the answer to user
    //and sets data into save container (have yet to implement)
    public void GiveReply(int i)
    {
        if (i == 1)
        {
            questionText.text = selectedItem.goodAnswer;
            closeButton.SetActive(true);
            //advice.Add(selectedItem.goodAnswer);
        }
        if (i == 2)
        {
            questionText.text = selectedItem.badAnswer;
            closeButton.SetActive(true);
            advice.Add(selectedItem.badAnswer);
            shortDesc.Add(selectedItem.shortDesc);
            //saves data here

        }
        //disables reply buttons
        answer1.SetActive(false);
        answer2.SetActive(false);

    }

    //turns of question panel and all the buttons 
    public void DisableQuestion()
    {
        closeButton.SetActive(false);
        answer1.SetActive(true);
        answer2.SetActive(true);
        questionPanel.SetActive(false);
    }

    //displays canvas
    public void IslandWellcome(string msg)
    {
        questionPanel.SetActive(true);
        questionText.text = msg;
        closeButton.SetActive(true);
    }

}
