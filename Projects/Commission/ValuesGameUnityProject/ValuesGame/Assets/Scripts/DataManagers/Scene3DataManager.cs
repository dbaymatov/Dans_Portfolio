using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Scene3DataManager : MonoBehaviour
{
    [SerializeField] float timeForQuestion;
    [SerializeField] Button Choice1;
    [SerializeField] Button Choice2;
    [SerializeField] TextMeshProUGUI button1Text;
    [SerializeField] TextMeshProUGUI button2Text;
    [SerializeField] TextMeshProUGUI levelCounter;

    [SerializeField] BackdropManager backdropManager;
    [SerializeField] GameObject questionsPanel;
    [SerializeField] GameObject levelEndPanel;
    [SerializeField] List<TextMeshProUGUI> resultsList;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] MoevementManagerScene3 mv;
    [SerializeField] Animator animator;
    public int character = 0;
    int selectedIndex;
    float timer;

    public List<QuestionPair> questionList;
    List<ValueClass> valuesList;
    List<ValueClass> pickedValues;
    SaveContainer sc;

    public int selectedCharacter;
    public bool countTime;
    public bool gameEnd;
    public bool gameOn;
    public bool showInstructions = false;
    [SerializeField] string pantherUrl, birdUrl;
    //collecting data from savefiles and initalizing all variables
    void Start()
    {
        pickedValues = new List<ValueClass>();
        gameEnd = false;
        gameOn = false;
        LoadData();
        Initialise();
        GameOff();

        videoPlayer.prepareCompleted += PrepareCompleted;
    }
    //checking timer
    void Update()
    {
        if (countTime)
        {
            timer += Time.deltaTime;

            if (timer >= timeForQuestion)
            {
                TimeOut();
            }
        }
    }
    private void PrepareCompleted(VideoPlayer source)
    {
        Debug.Log("prep completed");
        animator.SetTrigger("Crossfade_End");
        videoPlayer.Play();
    }
    public void GameOn()
    {
        gameOn = true;
        countTime = true;
        questionsPanel.SetActive(true);
    }
    public void GameOff()
    {
        gameOn = false;
        countTime = false;
        questionsPanel.SetActive(false);
    }
    //loads level 2 results from the file
    public void SaveData(List<ValueClass> topValues)
    {
        sc.saveList3 = topValues; //seting level 3 for saving
        sc.gameStage = 7;
        Stream stream = File.Open("SaveFiles.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, sc);
        stream.Close();
    }
    //using preveous save as basis, it will add data from 2nd level to save container
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
            SceneManager.LoadScene("MainMenu");
            ;
        }
        showInstructions = true;
    }

    void Initialise()
    {
        mv.InitialiseCharacter(sc.character);
        character = sc.character;
        valuesList = SortByValue(sc.saveList2, 7);
        PopulateResults();
        questionList = GenereateQuestionList(valuesList);
        selectedIndex = PickNewRandomValue();
        UpdateInterface();
        showInstructions = true;
        if (character == 1)
        {
            videoPlayer.url = pantherUrl;
        }
        else
        {
            videoPlayer.url = birdUrl;
        }
        videoPlayer.Prepare();
    }

    //sorts the list from biggest to smallest values and returns the top n values from it
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

    //algorythm used to generate list of questions from the sorted list of values
    public List<QuestionPair> GenereateQuestionList(List<ValueClass> listValue)
    {
        List<QuestionPair> questionList = new List<QuestionPair>();


        for (int i = 0; i < listValue.Count;) // -1 is because the last value in the array by the end would allready be paired to everything
        {
            ValueClass value1 = listValue[i];

            for (int j = i + 1; j < listValue.Count;)
            {
                //selects 2nd question and adds is to question list
                QuestionPair question = new QuestionPair(value1, listValue[j]);
                questionList.Add(question);
                j++;
            }
            i++;
        }

        //randomly swaps value places in the question
        for (int i = 0; i < questionList.Count;)
        {

            int random = UnityEngine.Random.Range(0, 2);

            if (random == 1)
            {
                ValueClass temp = questionList[i].Value1;
                questionList[i].Value1 = questionList[i].Value2;
                questionList[i].Value2 = temp;
            }
            i++;
        }

        return questionList;
    }
    //picks random unanswered question index from the list
    int PickNewRandomValue()
    {
        List<QuestionPair> tempQuestionList = new List<QuestionPair>();
        List<int> tempQuestionListID = new List<int>();


        // the forloop populates temp list of competiteon values and preserves there id in different list
        for (int i = 0; i < questionList.Count;)
        {
            if (questionList[i].choice == 0)
            {
                tempQuestionList.Add(questionList[i]);
                tempQuestionListID.Add(i);
            }
            i++;
        }
        levelCounter.text = 21-tempQuestionList.Count + "/21";
        Debug.Log(tempQuestionList.Count);
        int random = UnityEngine.Random.Range(0, tempQuestionList.Count - 1);
        return tempQuestionListID[random];
    }

    //checks if all questions have been answered
    bool AllQuestionsAnswered()
    {
        for (int i = 0; i < questionList.Count;)
        {
            if (questionList[i].choice == 0)
                return false;

            i++;
        }
        return true;
    }

    //updates button text and resets timer
    void UpdateInterface()
    {
        QuestionPair question = questionList[selectedIndex];

        timer = 0;
        button1Text.text = question.Value1.valueName;
        button2Text.text = question.Value2.valueName;

    }

    //calls update interface and random question index
    void TimeOut()
    {
        selectedIndex = PickNewRandomValue();
        UpdateInterface();
        EventManager.TimeOutBroadcast.Invoke();
        GameOff();

    }

    //method called whenever button clicked and sets data for the choice
    public void ChoiceClick(int choice)
    {
        if (choice == 1)
        {
            questionList[selectedIndex].choice = 1;
            pickedValues.Add(questionList[selectedIndex].Value1);
        }
        if (choice == 2)
        {

            questionList[selectedIndex].choice = 2;
            pickedValues.Add(questionList[selectedIndex].Value2);
        }

        //here it saves values and transitions to next scene once all the question are answered
        if (AllQuestionsAnswered())
        {
            gameEnd = true;
            List<ValueClass> topSelectedValues = GenerateTopValues(pickedValues, valuesList);
            SaveData(topSelectedValues);
            PopulateResults();
        }
        else
        {
            //else it moves on to next question
            selectedIndex = PickNewRandomValue();
            UpdateInterface();
        }
        GameOff();//turns of interface once button clicked answered
    }

    //takes values data from current level and picks top 8 valeus based of there picked frequency
    List<ValueClass> GenerateTopValues(List<ValueClass> chosenValues, List<ValueClass> questionedValues)
    {

        //this for loops will go through user picked answers and based on there frequency add value to the questions present in session
        for (int i = 0; i < questionedValues.Count;)
        {
            questionedValues[i].value = 0;//resets each values value

            for (int j = 0; j < chosenValues.Count;)
            {
                if (questionedValues[i].valueName == chosenValues[j].valueName)
                {
                    questionedValues[i].value++;
                }
                j++;
            }
            i++;
        }

        //will sort the list and pick top 8 values from it
        chosenValues = SortByValue(questionedValues, 5);
        return chosenValues;
    }

    //generates top 5 values for the endgame results screen
    void PopulateResults()
    {
        List<ValueClass> chosenValues = SortByValue(valuesList, 5);

        for (int i = 0; i < 5;)
        {
            resultsList[i].text = chosenValues[i].valueName;
            i++;
        }

    }
}