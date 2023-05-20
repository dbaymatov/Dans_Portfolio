using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
public class PlayScript : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject TitleText;
    Transform cameraTransform;
    [SerializeField] GameObject cameraPan;
    [SerializeField] float panSpeed;
    [SerializeField] public Transform startMarker;
    [SerializeField] public Transform endMarker;
    [SerializeField] public Transform[] locations;
    [SerializeField] public GameObject[] dialogueBubbles;
    [SerializeField] private bool transition = false;
    [SerializeField] GameObject wheel;
    [SerializeField] WheelScript spinner;
    [SerializeField] GameObject spinButton;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject satisfactionScrollBar;
    [SerializeField] GameObject smartGoals;
    [SerializeField] ChoicesScript choice;
    [SerializeField] ValueScript valueChoice;
    [SerializeField] public string[] valueNames; 
    [SerializeField] GameObject skipButton;
    [SerializeField] GameObject tutorialObject, tutorialCont, tutorialSkip, tutorialNext, tutorialText, t1, t2, t3; //beginning tutorial
    [SerializeField] MenuManager menuManager;
    [SerializeField] List<GameObject> slides;
    [SerializeField] List<GameObject> slideUIs;
    [SerializeField] AudioController audioSource;
    int currentSlide;


    [SerializeField]SpinScript spinny;
    public SaveContainer myContainer;

    private float startTime;
    int movement = 0;
    int tutorial = 0;
    bool begin = false;
    void Start()
    {
        // Application.targetFrameRate = 60;
        cameraTransform = cameraPan.transform;
        currentSlide = 0;
        audioSource.EnableAudio(false);
    }

    public void StartGame()
    {
        satisfactionScrollBar.SetActive(false);
       // continueButton.SetActive(false);
        playButton.SetActive(false);
        TitleText.SetActive(false);
        tutorialObject.SetActive(true);
        myContainer = new SaveContainer();//replaceing old data with empty container
        SaveData();
    }

    public void Tutorial()
    {
        if(tutorial == 0)
        {
            tutorialCont.SetActive(false);
            tutorialSkip.SetActive(false);
            tutorialText.SetActive(false);
            tutorialNext.SetActive(true);
            t1.SetActive(true);
        }
        else if(tutorial == 1)
        {
            t1.SetActive(false);
            t2.SetActive(true);
        }
        else if(tutorial == 2)
        {
            t2.SetActive(false);
            t3.SetActive(true);
        }
        else
        {
            t3.SetActive(false);
            PlayClick();
        }
        tutorial += 1;
    }


    public void PlayClick(){
        slideUIs[currentSlide].SetActive(false);
        playButton.SetActive(false);
        satisfactionScrollBar.SetActive(false);
        if (begin)
        {
            dialogueBubbles[movement - 1].SetActive(false);
        }
        begin = true;
        StartCoroutine(TransitionAnimation());
        tutorialObject.SetActive(false);    
        if (currentSlide > 0 && currentSlide< 8)
        {
            myContainer.AddValue();
            myContainer.v[movement - 1].changeRating(choice.value);
            myContainer.v[movement - 1].changeName(valueNames[movement - 1]);
            myContainer.v[movement - 1].changeID(movement - 1);
            SaveData();
            choice.value = 0;
        }
        else if(currentSlide>7)
        {
            myContainer.AddValue();
            myContainer.v[movement - 1].changeRating(choice.value);
            myContainer.v[movement - 1].changeName(valueNames[movement - 1]);
            myContainer.v[movement - 1].changeID(movement - 1);
            SaveData();
            choice.value = 0;

            currentSlide++;
            slides[currentSlide-1].SetActive(false);
            slides[currentSlide].SetActive(true);
            wheel.SetActive(false);
            spinButton.SetActive(false);
        }
    }

    public void skipClick()
    {
        Debug.Log(myContainer.v.Count+" counted values");
        if (myContainer.v.Count > 0)
        {
            for(int i = 0; i<myContainer.v.Count; i++)
            {
                choice.choiceValues[i] = myContainer.v[i].getRating();
            }

            slides[0].SetActive(false);
            currentSlide = myContainer.v.Count;
            slides[currentSlide].SetActive(true);
            slideUIs[currentSlide].SetActive(false);

            wheel.SetActive(false);

            movement = myContainer.v.Count;

            if (currentSlide < 7)
            {
                spinny.pick = movement;
                StartCoroutine(TransitionAnimation());
            }

            continueButton.SetActive(false);
            playButton.SetActive(false);
        }

    }

    public void HomeClick()
    {
        currentSlide = 0;
        startMarker = cameraTransform;
        endMarker = locations[0];
        movement = 0;
        satisfactionScrollBar.SetActive(false);
        spinButton.SetActive(false);
        playButton.SetActive(true);
        TitleText.SetActive(true);
        smartGoals.SetActive(false);
        transition = true;

    }

    public void SaveClick()
    {
        for (int i = 0; i < valueChoice.answers.Length; i++)
        {
            if (valueChoice.answers.Length != myContainer.answers.Count)
            {
                myContainer.AddAnswer();
            }
            myContainer.answers[i].smart = valueChoice.answers[i];
        }
        myContainer.ansName = valueChoice.finalValue;
        SaveData();
    }

    public void NextClick()
    {
        movement += 1;
        if (movement < locations.Length)
        {
            currentSlide++;
            startMarker = cameraTransform;
            endMarker = locations[movement];
            choice.selection = movement - 1;
            startTime = Time.time;
        }

    }

    public void exitGame()
    {
        Application.Quit();
    }
    
    IEnumerator TransitionAnimation(){
        wheel.SetActive(false);
        wheel.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        if (movement < 8)
        {
            wheel.GetComponent<Animator>().enabled = false;
            spinButton.SetActive(true);
        }
        else
        {
            movement = 9;
            endMarker = locations[9];
            transition = true;
        }
        startTime = Time.time;
    }
    void Update()
    {
        StartCoroutine(SpinWheel());
    }

    IEnumerator SpinWheel()
    {
        if (spinner.rotSpeed <= 0.1f && spinner.spin)
        {
            yield return StartCoroutine(SpinThenWait());            
            transition = true;
        }
        if (transition == true)
        {
            yield return StartCoroutine(Transition());
        }
        if(cameraTransform.position == endMarker.position)
        {
            transition = false;
        }
        Time.timeScale = 1.0f;
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0.05f);
        cameraTransform.position = endMarker.position;
        slides[currentSlide-1].SetActive(false);
        slides[currentSlide].SetActive(true);
        if (currentSlide >0 && currentSlide<slides.Count-1)
        {
            satisfactionScrollBar.SetActive(true);
        }
    }

    IEnumerator SpinThenWait()
    {
        yield return new WaitForSeconds(0.75f);
        wheel.GetComponent<Animator>().enabled = true;
        spinner.spin = false;
        wheel.SetActive(false);
        spinButton.SetActive(false);
    }

    public void SaveData()
    {
        Stream stream = File.Open("SaveLoadData.xml", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveContainer));
        serializer.Serialize(stream, myContainer);
        stream.Close();
        Debug.Log("data Saved");
    }
}
