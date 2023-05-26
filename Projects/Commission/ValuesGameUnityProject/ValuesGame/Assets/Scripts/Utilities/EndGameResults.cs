using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndGameResults : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] GameObject resultsScreen;
    [SerializeField] GameObject finalResultsScreen;
    [SerializeField] GameObject VideoPlayer;
    [SerializeField] GameObject Section2, Section3, sec1But, sec2But;
    [SerializeField] List<TextMeshProUGUI> resultsList;
    [SerializeField] List<TextMeshProUGUI> resultsList2;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string pantherUrl;
    [SerializeField] string birdUrl;
    [SerializeField] Animator animator;

    SaveContainer sc;
    List<ValueClass> valuesList;
    public GameObject menuButton;
    bool buttonPress;
    // Start is called before the first frame update
    void Start()
    {
        buttonPress = false;
        LoadData();
        videoPlayer.prepareCompleted += PrepareCompleted;
    }

    private void PrepareCompleted(VideoPlayer source)
    {
        Debug.Log("prep completed");
        animator.SetTrigger("Crossfade_End");
        videoPlayer.Play();
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
            SceneManager.LoadScene("MainMenu");
            ;
        }

        valuesList = sc.saveList3;
        valuesList = SortByValue(valuesList, 5);
        PopulateResults();

        if (!string.IsNullOrEmpty(pantherUrl) && !string.IsNullOrEmpty(birdUrl))
        {
            Debug.Log("string not empty");
            if (sc.character == 1)
            {

                videoPlayer.url = pantherUrl;
            }
            else
            {
                Debug.Log("bird link loaded");

                videoPlayer.url = birdUrl;
            }
        }
        videoPlayer.Prepare();
    }
    //takes in list of loaded values and sorts by its importance then exports the top n of them
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
        List<ValueClass> chosenValues = SortByValue(valuesList, 5);

        for (int i = 0; i < 5;)
        {
            resultsList[i].text = chosenValues[i].valueName;
            resultsList2[i].text = chosenValues[i].valueName;
            i++;
        }

    }
    IEnumerator ShowContinueButton()
    {
        yield return new WaitForSeconds(timer + 15);
        menuButton.SetActive(true);
    }
    IEnumerator StopVideo()
    {
        yield return new WaitForSeconds(25);
        VideoPlayer.SetActive(false);
    }
    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void NextScreen()
    {
        finalResultsScreen.SetActive(true);
        resultsScreen.SetActive(false);
    }
    public void NextPanel()
    {
        if (buttonPress)
        {
            sec2But.SetActive(false);
            Section3.SetActive(true);
            StartCoroutine(ShowContinueButton());
        }
        else
        {
            sec1But.SetActive(false);
            Section2.SetActive(true);
            buttonPress = true;
        }
    }
}
