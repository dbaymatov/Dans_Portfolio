using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] bool localBuild;
    [SerializeField] GameObject startNewGameButton;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject GameManagers;
    [SerializeField] PlayScript playScript;

    public SaveContainer sc = new SaveContainer();
    int currentStage;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        playScript.myContainer = sc;
        DisplayInterface();
    }

    public void DisplayInterface()
    {
        GameManagers.SetActive(true);
        currentStage = sc.v.Count;
        Debug.Log(currentStage +  " current stage from loaded data");

        if (currentStage >= 1 && currentStage != 8)//4 is level 1 game stage
        {
            if (currentStage < 7)
            {
                continueButton.SetActive(true);
            }
            startNewGameButton.SetActive(true);

        }
        else if (currentStage == 7)//7 is the results screen scene
        {
            startNewGameButton.SetActive(true);
            //ShowResults.SetActive(true);  HERE CAN WRITE MSG THAT THE GAME HAVE ALLREADY BEEN COMPLETED
        }
        else
        {
            startNewGameButton.SetActive(true);
        }
    }



    public void SaveData()
    {
        sc = new SaveContainer();
        Stream stream = File.Open("SaveLoadData.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, sc);
        stream.Close();
    }
    public void LoadData()
    {
        try
        {
            Stream stream = File.Open("SaveLoadData.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
            sc = (SaveContainer)serializer.Deserialize(stream);
            stream.Close();
        }

        catch
        {
            SaveData();
        }
    }


}