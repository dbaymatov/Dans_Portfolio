using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Main Menu Controller by Dan B 2/2/2022
    /// The script contains methods that are attached to the menu buttons,
    /// they will be responsible fo changing sound icon button,
    /// turning on/off instruction panel
    /// and transitioning to the selected scene.
    /// </summary>

    SaveFile sc;

    [SerializeField] GameObject ContinueButton, ResultsButton, StartButton;
    bool startNewGame = false;

    private void Start()
    {
        LoadData();
        LoadInterface();
    }

    //transitions to a different scene given the scene ID
    public void TranstitionToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //accesses the file and resets data
    public void StartNewGame()
    {
        sc.Initialise();
        SaveData();
        SceneManager.LoadScene(1);

    }

    //closes game on activation
    public void CloseGame()
    {
        Application.Quit();
    }


    public void LoadInterface()
    {
        ContinueButton.SetActive(false);
        ResultsButton.SetActive(false);

        if (sc.allIslands)
        {
            ContinueButton.SetActive(false);
            ResultsButton.SetActive(true);
        }

        Debug.Log("tutorial is " + sc.tutorial);
        if (sc.tutorial && !sc.allIslands)
        {
            ContinueButton.SetActive(true);
        }

    }

    //Save the streamingDataObject to xml
    public void SaveData()
    {
        //Create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));             //Create serializer
        FileStream stream = new FileStream("SaveFiles.xml", FileMode.Create); //Create file at this path
        serializer.Serialize(stream, sc);//Write the data in the xml file
        stream.Close();//Close the stream
    }

    //Load xml file
    public void LoadData()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            FileStream stream = new FileStream("SaveFiles.xml", FileMode.Open);
            sc = serializer.Deserialize(stream) as SaveFile;
            stream.Close();
        }
        catch
        {
            sc= new SaveFile();
            sc.Initialise();
            SaveData();
        }

    }

}
