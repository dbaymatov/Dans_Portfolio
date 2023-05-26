using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneIntroControlls : MonoBehaviour
{
    [SerializeField] GameObject playerView;
    [SerializeField] List<TimeAndPlace> camLocations;
    [SerializeField] List<string> cutsceneText;
    [SerializeField] List<float> textTime;//for each text the should be corresponding time it will be showing during cutscene
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CameraFollow cam;
    int currentLocation;
    int currentText;

    float currentCamTime;
    float currentTextTime;


    // Start is called before the first frame update
    void Start()
    {
        currentCamTime = currentTextTime = 0;
        ChangeCamLocation();
        ChangeText();
    }

    // Update is called once per frame
    void Update()
    {

        currentCamTime -= Time.deltaTime;
        currentTextTime -= Time.deltaTime;

        if (currentTextTime <= 0 && currentText < cutsceneText.Count - 1)
        {
            currentText++;
            ChangeText();

        }

        if (currentCamTime <= 0 && currentLocation < camLocations.Count - 1)
        {
            currentLocation++;
            ChangeCamLocation();

        }

        if (currentCamTime <= 0 && currentLocation == camLocations.Count - 1)
        {
            CutSceneEnd();
        }
    }

    void ChangeCamLocation()
    {
        currentCamTime = camLocations[currentLocation].time;
        transform.position = camLocations[currentLocation].positions.transform.position;
        transform.rotation = camLocations[currentLocation].positions.transform.rotation;
    }

    void ChangeText()
    {
        text.text = cutsceneText[currentText];
        currentTextTime = textTime[currentText];
    }

    void CutSceneEnd()
    {
        transform.position = playerView.transform.position;
        transform.rotation = new Quaternion(0, 0, 0, 1);
        text.enabled = false;
        enabled = false;
        cam.enabled = true;
    }
}
