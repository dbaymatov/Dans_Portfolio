using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandComplete : MonoBehaviour
{
    [SerializeField] GameObject CompletePanel; //Game Panel with "Well Done" message

    void Start()
    {
        CompletePanel.SetActive(false);//Disabling panel if incomplete
    }

    public void IslandFinish()
    {
        CompletePanel.SetActive(true);//Enabling Panel if complete
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(1);//button to return to menu
    }
}
