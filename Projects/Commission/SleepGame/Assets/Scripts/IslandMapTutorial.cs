using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandMapTutorial : MonoBehaviour
{
    int currentInstruction = 0;
    [SerializeField] IslandTracker tracker;
    [SerializeField] List<GameObject> Instructions;
    [SerializeField] GameObject LastInstruction;
    [SerializeField] GameObject ProtectionPanel;
    [SerializeField] GameObject ProgressText;
    [SerializeField] GameObject ShowResultsButton; // Display this button on all completed
    [SerializeField] GameObject Tutorial;
    public SaveFile saveContainer;

    void Start()
    {
        LoadData();
        InitialiseTutorial();
    }

    public void ActivateNextInstruction()
    {
        Instructions[currentInstruction].SetActive(false);
        //here it starts runing the game when it reaches last intruction click
        if (currentInstruction + 1 >= Instructions.Count)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
                ProtectionPanel.SetActive(false);
            LastInstruction.SetActive(true);
        }
        else
        {
            Instructions[currentInstruction + 1].SetActive(true);
            currentInstruction++;
        }
    }




    private void OnSentRequest(bool success, string container) { } // Why is this here?

    public void LoadData()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            FileStream stream = new FileStream("SaveFiles.xml", FileMode.Open);
            saveContainer = serializer.Deserialize(stream) as SaveFile;
            stream.Close();
        }
        catch //if data loading breaks sends back to the main menu
        {
            saveContainer = new SaveFile();
            saveContainer.Initialise();
            SceneManager.LoadScene(0);
        }
    }

    void InitialiseTutorial()
    {
        if (saveContainer.tutorial)
        {
            Tutorial.SetActive(false);
        }

        //counter handling
        int islandCheck = 0, islandCount = saveContainer.islandDataList.Count - 1;//the last island is used as container to store data for results screen
        for (int i = 0; i < islandCount; i++)
        {
            if (saveContainer.islandDataList[i].complete)
            {
                islandCheck += 1;
            }
        }

        ProgressText.GetComponent<TextMeshProUGUI>().text = islandCheck + " / 5";

        //checks wheter to activate results island
        if (islandCheck >= islandCount)
        {
            ShowResultsButton.SetActive(true);
        }

        //initializing island tracker script
        tracker.container = saveContainer;
        tracker.gameObject.SetActive(true);
    }

    public void TranstitionToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
