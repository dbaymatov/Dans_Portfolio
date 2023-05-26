using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    Sprite[] soundIcons;
    [SerializeField]
    Button mute;
    [SerializeField]
    GameObject tutorial, home;
    [SerializeField]
    GameObject[] tutorialPanels;
    int soundType;

    private void Start()
    {
        soundType = 1;
    }

    public void Home()
    {
        Time.timeScale = 0;
        home.SetActive(true);
    }
    public void Resume()
    {
        home.SetActive(false);
        Time.timeScale = 1;
    }
    public void End()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Mute()
    {
        if(soundType == 0)
        {
            mute.image.sprite = soundIcons[1];
            AudioListener.pause = false;
            soundType = 1;
        }
        else
        {
            mute.image.sprite = soundIcons[0];
            AudioListener.pause = true;
            soundType = 0;
        }
    }
    public void Help()
    {
        Time.timeScale = 0;
        tutorial.SetActive(true);
        tutorialPanels[0].SetActive(true);
        
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(6);
    }
}
