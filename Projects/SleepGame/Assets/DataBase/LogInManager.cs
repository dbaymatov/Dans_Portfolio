using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LogInManager : MonoBehaviour
{
    [SerializeField] GameObject logInScreen;
    [SerializeField] GameObject Instructions;
    [SerializeField] TMP_InputField InputName;
    [SerializeField] GameObject mainMenuPanel;
    public SaveFile sc = new SaveFile();


    string KeyUserName = "amrit";
    string KeyUserPassword = "Af(%7!yl1IdXoE@n";

    void Start()
    {
        sc.Initialise();
        SetUpDelegate(true);
        if (Connector.Instance.loggedIn)
        {
            SetUpDelegate(false);
            mainMenuPanel.SetActive(true);
            //Instructions.SetActive(true);
            logInScreen.SetActive(false);

        }
        //else
        //{
        //    Instructions.SetActive(false);
        //    logInScreen.SetActive(true);
        //}
    }

    void SetUpDelegate(bool add)
    {
        if (add)
        {
            Connector.Instance.OnAuth += OnAuth;
            Connector.Instance.OnLoggedIn += OnLoggedIn;//make a hook to this instance
            Connector.Instance.OnError += OnError;
            Connector.Instance.OnSentRequest += OnSentRequest;
            Connector.Instance.OnGetRequest += OnGetRequest;
        }
        else
        {
            Connector.Instance.OnAuth -= OnAuth;
            Connector.Instance.OnLoggedIn -= OnLoggedIn;//remove the hooks from the files
            Connector.Instance.OnError -= OnError;
            Connector.Instance.OnSentRequest -= OnSentRequest;
            Connector.Instance.OnGetRequest -= OnGetRequest;
        }
    }




    private void OnAuth(bool success)
    {
        Debug.Log("On auth is " + success);

        if (success)
        {
            LoadData();
            Connector.Instance.loggedIn = true;
        }
        else
        {
            logInScreen.SetActive(true);
        }
    }

    void OnDestroy()
    {
        SetUpDelegate(false);
    }

    private void OnError(string error, string result)
    {
        Debug.Log($"{error} {result}");//TODO : ADD TXT FIEDL TO TELL USER WHAT BROKE
    }

    //here it will be checking the delegate variable, and depending on its value it wil
    private void OnLoggedIn(bool Success)
    {
        if (Success)
        {
            StartCoroutine(Connector.Instance.Auth());
        }
        else
        {
            logInScreen.SetActive(true);
        }
    }

    //disables login screen and starts the LogIn coroutine 
    public void Submit()
    {
        logInScreen.SetActive(false);
        //StartCoroutine(Connector.Instance.LogIn(InputName.text, InputPassword.text));
        StartCoroutine(Connector.Instance.LogIn(InputName.text, KeyUserName, KeyUserPassword));
    }

    private void OnGetRequest(bool success, string container)
    {
        Debug.Log("OnGet Request");

        if (success)
        {
            //string json = File.ReadAllText("JsonFile.json");
            sc = JsonUtility.FromJson<SaveFile>(container);
            if (sc == null)
            {
                SaveData();
            }
            else
            {
                Debug.Log("removed delegates");
                SetUpDelegate(false);
                Instructions.SetActive(true);
                SetUpDelegate(false);
            }
        }
        else
        {
            SaveData();
        }
    }

    private void OnSentRequest(bool success, string container)
    {
        if (success)
        {
            SetUpDelegate(false);
            Instructions.SetActive(true);
        }
    }
    public void LoadData()
    {
        Debug.Log("Loading Data");
        StartCoroutine(Connector.Instance.GetUserDetails());
    }
    void SaveData()
    {
        StartCoroutine(Connector.Instance.SendUserDetails(JsonUtility.ToJson(sc)));
    }

}

