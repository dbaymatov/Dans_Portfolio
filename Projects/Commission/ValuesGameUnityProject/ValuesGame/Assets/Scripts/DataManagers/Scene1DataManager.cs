using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine.Events;
using System;

static public class EventManager
{
    static public UnityEvent TimeOutBroadcast = new UnityEvent();
    public static UnityEvent ResetRock = new UnityEvent();
    public static UnityEvent EndGame = new UnityEvent();
}

public class Scene1DataManager : MonoBehaviour
{
    [SerializeField] List<ValueClass> valuesList;
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] TextMeshProUGUI valueDescription;
    [SerializeField] Slider valueSlider;
    [SerializeField] Button acceptButton;
    [SerializeField] GameObject gameOnInterface;
    [SerializeField] GameObject endFirstLevel;
    [SerializeField] List<TextMeshProUGUI> resultsList;
    SaveContainer sc;
    int selectedIndex;
    float timer;
    public bool startNewLevel = false;
    public bool showInstructions = false;
    public bool gameOn;
    public bool gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        GameOff();
        //setting values to start the game
        timer = 0;
        selectedIndex = 0;
        UpdateInterface();
    }
    
    //updates timer and checks if it is time out
    private void Update()
    {
        //if game is in progress time will be added
        Timer();
    }



    //turns interface on and off while also changing bool gameOn that influences timer method
    public void GameOn()
    {
        gameOnInterface.SetActive(true);
        gameOn = true;
    }
    public void GameOff()
    {
        gameOnInterface.SetActive(false);
        gameOn = false;
    }

    //adds time if game is in progress and calls TimeOut when time runs out
    void Timer()
    {
        if (gameOn)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 10)
        {
            TimeOut();
        }
    }

    //updates textbox and value slider
    void UpdateInterface()
    {
        timer = 0;
        valueText.text = valuesList[selectedIndex].valueName;
        valueDescription.text = valuesList[selectedIndex].valueDescription;
        valueSlider.value = 0;
    }

    //checks the list of questions and returns true if there are no values in competiteon
    bool AllValuesAnswered()
    {
        for (int i = 0; i < valuesList.Count;)
        {
            if (valuesList[i].valueInCompetiteon)

                return false;

            i++;
        }
        gameOn = false;//stops timer and turns of interface
        gameOnInterface.SetActive(false);//turns off interface

        return true;
    }

    //sets slider number to a selected value and selects a new value from the list, also removes value from competiteon
    public void ButtonClick()
    {

        valuesList[selectedIndex].value = valueSlider.value;
        valuesList[selectedIndex].valueInCompetiteon = false;
        GameOff();//turns of interface

        //here it saves values, hides the value objects and displays the end scene pop-up text
        if (AllValuesAnswered())
        {
            EndGame();
        }
        else
        {
            //else it moves on to next question
            selectedIndex = PickNewRandomValue();
            UpdateInterface();
        }
    }

    int PickNewRandomValue()
    {
        List<ValueClass> tempValueList = new List<ValueClass>();
        List<int> tempValueListID = new List<int>();


        // the forloop populates temp list of competiteon values and preserves there id in different list
        for (int i = 0; i < valuesList.Count;)
        {
            if (valuesList[i].valueInCompetiteon)
            {
                tempValueList.Add(valuesList[i]);
                tempValueListID.Add(i);
            }
            i++;
        }

        int random = UnityEngine.Random.Range(0, tempValueList.Count - 1);

        return tempValueListID[random];

    }

    void TimeOut()
    {
        //sets that values of a time runout trait to 0 and kicks it out of competiteon
        valuesList[selectedIndex].value = 0;
        valuesList[selectedIndex].valueInCompetiteon = false;
        GameOff();//turns of interface

        //here it saves values, hides the value objects and displays the end scene pop-up text
        if (AllValuesAnswered())
        {
            EndGame();
            Debug.Log("all values answered and the game is ended");
        }
        else
        {
            //else it moves on to next question
            selectedIndex = PickNewRandomValue();
            UpdateInterface();
        }
        EventManager.TimeOutBroadcast.Invoke();

    }

    public List<ValueClass> SortByValue(List<ValueClass> unsortedList, int n)
    {
        ValueClass temp;
        List<ValueClass> sortedList = new List<ValueClass>();

        for (int i = 0; i < unsortedList.Count;)
        {

            for (int j = i; j < unsortedList.Count;)
            {
                temp = unsortedList[j];

                if (unsortedList[i].value < unsortedList[j].value)
                {
                    temp = unsortedList[i];
                    unsortedList[i] = unsortedList[j];
                    unsortedList[j] = temp;
                }
                j++;
            }
            i++;
        }

        //populates the export list
        for (int i = 0; i < n;)
        {
            sortedList.Add(unsortedList[i]);
            i++;
        }
        return sortedList;
    }

    void PopulateResults()
    {
        List<ValueClass> chosenValues = SortByValue(valuesList, 9);

        for (int i = 0; i < 9;)
        {
            resultsList[i].text = chosenValues[i].valueName;
            i++;
        }

    }

    public void SaveData()
    {
        sc.saveList1 = valuesList;
        sc.gameStage = 5;
        sc.character = PlayerPrefs.GetInt("selectedCharacter"); ;
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
            SceneManager.LoadScene("MainMenu");
;        }
        showInstructions = true;
    }



    //here activates top 10 value panel with button to move next scene
    void EndGame()
    {
        StartCoroutine(ShowEndgameResults());
        gameEnd = true;
        SaveData();
        PopulateResults();
        EventManager.EndGame.Invoke();
        GameOff();
        Debug.Log("all questions answered");
    }
    IEnumerator ShowEndgameResults()
    {
        yield return new WaitForSeconds(8);
        endFirstLevel.SetActive(true);
        Time.timeScale = 0;
    }

}
