using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;



public class DataManager : MonoBehaviour
{
    //variables
    InventoryItem selectedItem;
    SaveFile saveContainer = new SaveFile();
    List<InventoryItem> loadItems;//representation of collectables within code
    int islandCount;
    public int islandTotal;

    //data variables
    [SerializeField] List<InventoryItem> sceneItems;
    [SerializeField] int islandID;//used to select right island data in savefiles
    [SerializeField] List<GameObject> collactables; //NOTE: collectables position in array must correspond to the item id
    [SerializeField] List<GameObject> inventorySlots;//reference inventory spaces here

    //interface
    [SerializeField] GameObject questionPanel;
    [SerializeField] TextMeshProUGUI buttonText1;
    [SerializeField] TextMeshProUGUI buttonText2;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject closeButton;
    [SerializeField] GameObject answer1;
    [SerializeField] GameObject answer2;
    [SerializeField] GameObject completeMessage;
    [SerializeField] GameObject progressText;

    private void Start()
    {
        islandCount = 0;
        LoadData();
        Initialise();
    }




    private void Initialise()
    {
        islandCount = saveContainer.islandDataList[islandID].islandCount;
        progressText.GetComponent<TextMeshProUGUI>().text = islandCount + " / " + islandTotal;
        loadItems = saveContainer.islandDataList[islandID].sceneItems;

        //if no data has been saved and user plays level first time it will use default data
        if (loadItems.Count == 0)
        {
            loadItems = sceneItems;
        }

        if (saveContainer.islandDataList[islandID].complete)
        {
            completeMessage.SetActive(true);
        }

        SetData();
        UpdateInventory();
    }



    public void LoadData()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            FileStream stream = new FileStream("SaveFiles.xml", FileMode.Open);
            saveContainer = serializer.Deserialize(stream) as SaveFile;
            stream.Close();
        }
        //if something broke, will send user to main menu. From there whatever loading issue happend should be fixed
        catch
        {
            
            TranstitionToScene(0);
        }
    }

    void SaveData()
    {
        Debug.Log(sceneItems.Count);

        int j = 0;
        for (int i = 0; i < sceneItems.Count;)
        {
            if (sceneItems[i].active)
            {
                j++;
            }
            i++;
        }

        saveContainer.islandDataList[islandID].sceneItems = loadItems;
        saveContainer.islandDataList[islandID].islandCount = islandCount;

        //Create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));             //Create serializer
        FileStream stream = new FileStream("SaveFiles.xml", FileMode.Create); //Create file at this path
        serializer.Serialize(stream, saveContainer);//Write the data in the xml file
        stream.Close();//Close the stream
    }

    //sets loaded or default data onto the interface
    void SetData()
    {
        Debug.Log("SettingData counted " + loadItems.Count);

        for (int i = 0; i < collactables.Count;)
        {
            if (!loadItems[i].active)
            {
                collactables[i].SetActive(false);
            }
            i++;
        }
    }

    //on item click this method actiates
    public void ItemClick(int itemID)
    {
        selectedItem = FindItemByID(sceneItems, itemID);
        DisplayQuestionare(selectedItem);
    }

    //displayes questions panel and asks question based on the clicked item
    public void DisplayQuestionare(InventoryItem chosenItem)
    {
        questionPanel.SetActive(true);
        questionText.text = selectedItem.question;
        buttonText1.text = selectedItem.questionAnsweres[0];
        buttonText2.text = selectedItem.questionAnsweres[1];
    }

    //after user picks asnwer a replay popup will show based on the answer
    public void GiveReply(int i)
    {
        if (i == 1)
        {
            questionText.text = selectedItem.goodAnswer;
            closeButton.SetActive(true);
            sceneItems[selectedItem.id].answeredCorrectly = true;

        }
        if (i == 2)
        {
            questionText.text = selectedItem.badAnswer;
            closeButton.SetActive(true);
            loadItems[selectedItem.id].answeredCorrectly = false;


        }
        //disables reply buttons and clicked object
        answer1.SetActive(false);
        answer2.SetActive(false);
        collactables[selectedItem.id].SetActive(false);

        loadItems[selectedItem.id].active = false;
        UpdateInventory();
    }

    //closes the question panel and resets interface for the next use
    public void DisableQuestion()
    {
        closeButton.SetActive(false);
        answer1.SetActive(true);
        answer2.SetActive(true);
        questionPanel.SetActive(false);
        islandCount += 1;
        progressText.GetComponent<TextMeshProUGUI>().text = islandCount + " / " + islandTotal;
        //checking to see how many items have been collected in order to activate complete message when done
        if (islandCount == sceneItems.Count)
        {
            completeMessage.SetActive(true);
            saveContainer.islandDataList[islandID].complete = true;
            if (SceneManager.GetActiveScene().buildIndex == 8)
            {
                saveContainer.tutorial = true;
                Debug.Log("saveContainer.tutorial");
            }
            int islandCheck = 0;
            for (int i = 0; i < 6; i++)
            {
                if (saveContainer.islandDataList[i].complete)
                {
                    islandCheck++;
                }
            }
            if (islandCheck == 6)
            {
                saveContainer.allIslands = true;
            }
            saveContainer.tutorial = true;

        }
    }

    //finds and draws object in the inventory based on its ID
    //NOTE: will cause error if the amount of collected items exceeds the number of slot fields
    void UpdateInventory()
    {
        int selectedSlot = 0;//will start drawing from slot 0

        for (int i = 0; i < sceneItems.Count;)
        {
            if (!loadItems[i].active)//if object is collected it will get drawn in the inventory
            {
                Image img = inventorySlots[selectedSlot].GetComponent<Image>();
                InventoryItem item = FindItemByID(sceneItems, i);
                img.enabled = true;
                img.sprite = item.itemTxt;
                selectedSlot++;//the selected slot will move on to the next space
            }
            i++;
        }
    }

    //looks through collectables list and returns the one with chosen ID
    InventoryItem FindItemByID(List<InventoryItem> itemList, int ID)
    {
        for (int i = 0; i < itemList.Count;)
        {
            if (ID == itemList[i].id)
            {
                return itemList[i];
            }
            i++;
        }
        return itemList[0];
    }

    public void TranstitionToScene(int sceneID)
    {
        SaveData();
        SceneManager.LoadScene(sceneID);
    }
   
}
