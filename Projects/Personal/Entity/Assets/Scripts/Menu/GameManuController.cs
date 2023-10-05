using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManuController : MonoBehaviour
{
    [SerializeField] GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuController();
            Debug.Log("menu worked");
        }
    }

    public void menuController()
    {

        if (menu.activeSelf == false)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }

    }


    public void closeMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("menu closed");
    }


}
