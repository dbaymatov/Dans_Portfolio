using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResultsScreenManager : MonoBehaviour
{
    SaveFile saveContainer;
    List<InventoryItem> ReccomendedAdviceList;
    List<InventoryItem> ChosenAdvice;
    List<Toggle> Toggles;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject listScreen;
    [SerializeField] GameObject chosenAdviceScreen, printButton, endScreen;

    [SerializeField] TextMeshProUGUI TipName;
    [SerializeField] TextMeshProUGUI TipDetails;
    [SerializeField] TextMeshProUGUI ErrMsg;

    [SerializeField] Button NextButton;

    int currentAdvice = 0;
    public int amountSelected = 0;
    bool finishedLookingTips=false;
    // Start is called before the first frame update
    void Start()
    {
        ReccomendedAdviceList = new List<InventoryItem>();
        ChosenAdvice = new List<InventoryItem>();
        Toggles = new List<Toggle>();
        LoadData();
        Initialise();
    }

    public void SaveData()
    {
        //Create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));             //Create serializer
        FileStream stream = new FileStream("SaveFiles.xml", FileMode.Create); //Create file at this path
        serializer.Serialize(stream, saveContainer);//Write the data in the xml file
        stream.Close();//Close the stream
    }

    //Load xml file
    public void LoadData()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            FileStream stream = new FileStream("SaveFiles.xml", FileMode.Open);
            saveContainer = serializer.Deserialize(stream) as SaveFile;
            stream.Close();
        }
        catch
        {
            saveContainer = new SaveFile();
            saveContainer.Initialise();
            SaveData();
        }
    }

    public void Initialise()
    {

        for (int i = 0; i < saveContainer.islandDataList.Count - 1;)//for each island, last island is result screen storage thats why -1
        {
            for (int j = 0; j < saveContainer.islandDataList[i].sceneItems.Count;)//for each object in the island
            {
                if (!saveContainer.islandDataList[i].sceneItems[j].answeredCorrectly)
                {
                    ReccomendedAdviceList.Add(saveContainer.islandDataList[i].sceneItems[j]);
                }
                j++;
            }
            i++;
        }

        for (int f = 0; f < ReccomendedAdviceList.Count;)
        {
            Debug.Log(ReccomendedAdviceList[f].shortDesc);
            f++;
        }


        for (int i = 0; i < ReccomendedAdviceList.Count;)
        {
            //creating checkboxes and setting them into my list
            GameObject temp = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.parent = listScreen.transform;
            temp.transform.localScale = new Vector3(1, 1, 1);

            //getting the checkboxes label text from child and setting desc into it
            Text txt = temp.GetComponentInChildren<Text>();
            txt.text = ReccomendedAdviceList[i].shortDesc;
            temp.GetComponent<CheckBoxController>().item = ReccomendedAdviceList[i];
            Toggles.Add(temp.GetComponent<Toggle>());

            i++;
        }

        //if user allready picked advice and going back to look at selected
        if (saveContainer.results)
        {
            ChosenAdvice = saveContainer.islandDataList[5].sceneItems; 
            chosenAdviceScreen.SetActive(true);
            UpdateTipResultInterface();
        }

    }

    public void CheckBoxSelect()
    {
        amountSelected++;
    }
    public void CheckBoxDeselect()
    {
        amountSelected--;
    }
    public bool CanSelectMore()
    {
        int amountSelected = 0;
        for (int i = 0; i < Toggles.Count;)
        {

            if (Toggles[i].isOn)
            {
                amountSelected++;
            }
            i++;
        }

        if (amountSelected > 3)
        {
            return false;
        }

        return true;
    }

    public void Submit()
    {

        Debug.Log("I activated");
        for (int i = 0; i < Toggles.Count; i++)
        {
            if (Toggles[i].isOn)
            {
                ChosenAdvice.Add(ReccomendedAdviceList[i]);//adds picked advice to the list of chosen
            }
        }
        SaveData();

        if (ChosenAdvice.Count == 0)
        {
            ErrMsg.text = "please select at least 1 advice";
        }
        else
        {
            //actiaveting interface for next screen
            chosenAdviceScreen.SetActive(true);
            saveContainer.islandDataList[5].sceneItems = ChosenAdvice;
            saveContainer.results = true;
            SaveData();
            UpdateTipResultInterface();
        }
        
    }

    public void UpdateTipResultInterface()
    {

        if (finishedLookingTips)
        {
            chosenAdviceScreen.SetActive(false);
            endScreen.SetActive(true);
        }

        else if (ChosenAdvice.Count > 0)
        {
            TipName.text = ChosenAdvice[currentAdvice].shortDesc;
            TipDetails.text = ChosenAdvice[currentAdvice].badAnswer;
            currentAdvice++;
        }
        else
        {
            TipName.text = "Well done";
            TipDetails.text = "You have completed the game, and asnwered all questions correctly. Thus you have a perfect sleep";
            finishedLookingTips = true;
            currentAdvice++;
        }

        if (currentAdvice == ChosenAdvice.Count)
        {
            finishedLookingTips = true;
        }
    }

    public void TranstitionToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
