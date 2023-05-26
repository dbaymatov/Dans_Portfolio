using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject startNewGameButton;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject ShowResults;
    [SerializeField] GameObject InstructionsPanel;
    [SerializeField] GameObject MainMenu;

    public SaveContainer sc = new SaveContainer();
    int currentStage;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    public void DisplayInterface()
    {
        currentStage = sc.gameStage;
        if (currentStage > 4 && currentStage != 7)//4 is level 1 game stage
        {
            continueButton.SetActive(true);
            startNewGameButton.SetActive(true);

        }
        else if (currentStage == 7)//7 is the results screen scene
        {
            startNewGameButton.SetActive(true);
            ShowResults.SetActive(true);
        }
        else
        {
            startNewGameButton.SetActive(true);
        }
    }

    //button methods bellow
    public void ContinueClick()
    {
        // SceneManager.LoadScene(currentStage);

        if (currentStage == 5)
        {
            SceneManager.LoadScene("Game2");

        }
        if (currentStage == 6)
        {
            SceneManager.LoadScene("Game3");

        }
        else
        {
            SceneManager.LoadScene("Game2");

        }
    }
    public void ShowResultsScreen()
    {
        SceneManager.LoadScene("Final Results");
    }

    public void NewGameClick()
    {
        startNewGameButton.SetActive(false);
        sc = new SaveContainer();
        SaveData();
        SceneManager.LoadScene("Character Selection");
    }

    public void SaveData()
    {
        Stream stream = File.Open("SaveFiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, sc);
        stream.Close();
    }
    public void LoadData()
    {
        try
        {
            Stream stream = File.Open("SaveFiles.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
            sc = (SaveContainer)serializer.Deserialize(stream);
            stream.Close();

            if (sc == null || sc.gameStage == 0)
            {
                throw new Exception();
            }
        }
        catch
        {
            sc = new SaveContainer();
            SaveData();
        }
    }

}
