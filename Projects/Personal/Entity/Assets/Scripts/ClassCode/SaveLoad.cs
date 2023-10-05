using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;



public class SaveLoad : MonoBehaviour
{
    SaveContainer myContainer;

    [SerializeField] Text leaderBoard;
    [SerializeField] InputField inputWord;
    [SerializeField] Clicker clicker;

    void Start()
    {
        myContainer = new SaveContainer();

        try
        {
            LoadData();

        }
        catch
        {
            myContainer.defualtData();
        }
    }

    void Update()
    {

        leaderBoard.text = myContainer.playerInput + " " + myContainer.number;

        if (clicker.clickCounter > myContainer.number)
        {
            SaveData();

        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc has been pressed");

        }

    }




    public void SaveData()
    {
        myContainer.playerInput = inputWord.text;
        myContainer.number = clicker.clickCounter;

        Stream stream = File.Open("SaveLoadData.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, myContainer);
        stream.Close();
    }



    public void LoadData()
    {
        Stream stream = File.Open("SaveLoadData.xml", FileMode.Open);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        myContainer = (SaveContainer)serializer.Deserialize(stream);
        stream.Close();

    }

}
[System.Serializable]
public class SaveContainer
{
    public string playerInput;
    public int number;

    public void defualtData()
    {
        playerInput = "Null";
        number = 0;

    }

}

