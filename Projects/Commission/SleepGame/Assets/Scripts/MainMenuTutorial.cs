using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class MainMenuTutorial : MonoBehaviour
{

    int currentInstruction=0;
    [SerializeField]List<GameObject> Instructions;
    [SerializeField] GameObject InstructionsPanel;
    [SerializeField] GameObject MainMenuPanel;
    SaveFile saveContainer;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        bool tutorial = true;

        //looks through islands if checks if any are completed
        for (int i = 0; i < saveContainer.islandDataList.Count;)
        {
            if (saveContainer.islandDataList[i].complete)
            {
                tutorial = false;
            }
            i++;
        }

        //deicdes whether to actiate/ deactivate the tut based on the forloop result
        if (!tutorial)
        {
            Debug.Log("Tutorial is enabled");
            MainMenuPanel.SetActive(true);
            InstructionsPanel.SetActive(false);
        }
     
    }

    public void ActivateNextInstruction()
    {
        Instructions[currentInstruction].SetActive(false);
        //here it starts runing the game when it reaches last intruction click
        if (currentInstruction + 1 >= Instructions.Count)
        {
            InstructionsPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }

        else
        {
            Instructions[currentInstruction + 1].SetActive(true);
            currentInstruction++;
        }
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
        catch
        {
            saveContainer = new SaveFile();
            saveContainer.Initialise();
        }
    }
}
