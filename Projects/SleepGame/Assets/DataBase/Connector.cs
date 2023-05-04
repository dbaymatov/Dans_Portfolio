using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Connector : MonoBehaviour
{
    public static Connector Instance;

    public delegate void OnLoggedInDelegate(bool success);
    public delegate void OnErrorDelegate(string error, string result);
    public delegate void OnGetRequestDelegate(bool success, string container);
    public delegate void OnSentRequestDelegate(bool success, string container);
    public delegate void OnAuthDelegate(bool success);

    public OnErrorDelegate OnError;

    public OnLoggedInDelegate OnLoggedIn;
    public OnGetRequestDelegate OnGetRequest;
    public OnSentRequestDelegate OnSentRequest;
    public OnAuthDelegate OnAuth;

    [Header("First Server Related")]
    public string baseUrl;
    public string loginUrl;

    [Header("Second Server Related")]
    public string baseUrl2;
    public string authUrl;
    public string userUrl;
    public string apiKey;
    public string apiKeyLocal;
    public string apiKeyServer;


    [Header("User details")]
    public string userName;
    public string token;

    [Header("LoggedIn")]
    public bool loggedIn=false;
    public bool InTheGame=false;

    [TextArea]
    public string Text;

    enum ServerState
    {
        none,
        logIn,
        auth,
        getRequest,
        sendRequest
    }

    public bool CommunicatingServer = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
#if UNITY_EDITOR
        apiKey = apiKeyLocal;
#else
        apiKey = apiKeyServer;
#endif

    }


    public IEnumerator LogIn(string userName, string keyUser, string keyPassword)
    {
        //waits until preveous communication ended to start a new one
        if (CommunicatingServer)
            yield return new WaitUntil(() => !CommunicatingServer);
        Dictionary<string, string> dataToSend = new Dictionary<string, string>();//ask why theres 2 strings, wats Tkey Tvalue?
        dataToSend.Add("username", keyUser);//why add function is used, is it a list?
        dataToSend.Add("password", keyPassword);
        this.userName = userName;
        StartCoroutine(DoServerCommunication(ServerState.logIn, baseUrl + loginUrl, dataToSend));//here it says what action to perform, serv url and info to send

    }

    public IEnumerator Auth()
    {
        //waits until preveous communication ended to start a new one
        if (CommunicatingServer)
            yield return new WaitUntil(() => !CommunicatingServer);
        Dictionary<string, string> dataToSend = new Dictionary<string, string>();//ask why theres 2 strings, wats Tkey Tvalue?
        dataToSend.Add("username", this.userName);//why add function is used, is it a list?
        dataToSend.Add("api_key", this.apiKey);

        StartCoroutine(DoServerCommunication(ServerState.auth, baseUrl2 + authUrl, dataToSend));//here it says what action to perform, serv url and info to send

    }


    public IEnumerator GetUserDetails()
    {
        //waits until preveous communication ended to start a new one
        if (CommunicatingServer)
            yield return new WaitUntil(() => !CommunicatingServer);

        StartCoroutine(DoServerCommunication(ServerState.getRequest, baseUrl2 + userUrl + "?api_key=" + this.apiKey+ "&username=" + this.userName));//here it says what action to perform, serv url and info to send
    }

    public IEnumerator SendUserDetails(string container)
    {
        //waits until preveous communication ended to start a new one
        if (CommunicatingServer)
            yield return new WaitUntil(() => !CommunicatingServer);
        Dictionary<string, string> dataToSend = new Dictionary<string, string>();

        dataToSend.Add("username", this.userName);
        dataToSend.Add("container3", container);
        dataToSend.Add("api_key", this.apiKey);

        StartCoroutine(DoServerCommunication(ServerState.sendRequest, baseUrl2 + userUrl, dataToSend));//here it says what action to perform, serv url and info to send
    //    StartCoroutine(DoServerCommunicationString(ServerState.sendRequest, baseUrl + userUrl, JsonUtility.ToJson(acf)));//here it says what action to perform, serv url and info to send
    }



    IEnumerator DoServerCommunicationString(ServerState serverState, string url, string data = null)
    {
        CommunicatingServer = true;//declaring that a communication coroutine is ongoing
        UnityWebRequest www = new UnityWebRequest(url);//creates a web connection to the server given the url

        if (data != null)//if data empty, asks for data
        {
            
            www = UnityWebRequest.Put(url, data);//sending data
        }
        else//else asks for data
        {
            www = UnityWebRequest.Get(url);//collecting data
        }

        if (!String.IsNullOrEmpty(token))
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);
        }

        yield return www.SendWebRequest();//waits until web request have finished
        CommunicatingServer = false;//declares and end to communication

        //after communicating with the server the err checking and handling starts or handling the receved data

        string resultString = www.downloadHandler.text;
        string errMsg = string.Empty;
        bool errHappend = false;

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("server comm err");
            Debug.Log($"error: {www.error}");
            Debug.Log($"response {resultString}");

            if (serverState == ServerState.logIn)
            {
                if (OnLoggedIn != null)
                {
                    OnLoggedIn(false);//declares the user in not logged in
                }
            }

            if (serverState == ServerState.auth)
            {
                if (OnAuth != null)
                {
                    OnAuth(false);//declares the user in not logged in
                }
            }

            if (serverState == ServerState.getRequest)
            {
                if (OnGetRequest != null)
                {
                    OnGetRequest(false, null);
                }
            }
            if (serverState == ServerState.sendRequest)
            {
                if (OnSentRequest != null)
                {
                    OnSentRequest(false, null);
                }
            }

        }
        else
        {
            Debug.Log($"response .. {resultString}");
            Text = resultString;
            if (serverState == ServerState.logIn)//here it checkes the log in data
            {
                LogInResponse logInResponse = new LogInResponse();//login responce is a data container class used to store json string packet from server

                try
                {
                    logInResponse = JsonUtility.FromJson<LogInResponse>(resultString);//tryes to read json string and make it into data for the build
                }
                catch
                {
                    logInResponse.code = "jwt_auth_failed";
                }

                if (!String.IsNullOrEmpty(logInResponse.token))//if token key is not empty
                {
                    if (OnLoggedIn != null)
                    {
                        OnLoggedIn(true);
                    }
                    token = logInResponse.token;//sets the local variable for token to be used for sending data
                }
                else//else it will make login failed
                {
                    if (OnLoggedIn != null)
                    {
                        OnLoggedIn(false);
                    }
                    errMsg = logInResponse.message;
                    errHappend = true;
                }
            }

            if (serverState == ServerState.auth)//here it checkes the log in data
            {
                AuthResponse authResponse = new AuthResponse();//login responce is a data container class used to store json string packet from server

                try
                {
                    authResponse = JsonUtility.FromJson<AuthResponse>(resultString);//tryes to read json string and make it into data for the build
                    Debug.Log("auth resp success" + authResponse.description);

                }
                catch
                {
                    Debug.Log("auth resp err "+authResponse.description);
                    authResponse.status = -1;
                }

                if (authResponse.status!=-1)//if token key is not empty
                {
                    if (OnAuth != null)
                    {
                        OnAuth(true);
                    }
                }
                else//else it will make login failed
                {
                    if (OnAuth!= null)
                    {
                        OnAuth(false);
                    }
                    errMsg = authResponse.description;
                    errHappend = true;
                }
            }


            if (serverState == ServerState.getRequest)
            {
                GetResponse get = new GetResponse();

                try
                {
                    get = JsonUtility.FromJson<GetResponse>(resultString);
                }
                catch
                {
                    get.status = -1;
                }

                if (OnGetRequest != null)
                {
                    if (String.IsNullOrEmpty(get.data.container3))
                    {
                        OnGetRequest(false, null);

                    }
                    else
                    {
                        OnGetRequest(true, get.data.container3);
                    }
                }
            }
            if (serverState == ServerState.sendRequest)
            {
                SendResponse set = new SendResponse();

                try
                {
                    set = JsonUtility.FromJson<SendResponse>(resultString);
                }
                catch
                {
                    set.status = -1;
                }

                if (OnSentRequest != null)
                {
                    if (String.IsNullOrEmpty(set.data.container3))
                    {
                        OnSentRequest(false, null);

                    }
                    else
                    {
                        OnSentRequest(true, set.data.container3);
                    }
                }
            }

            if (errHappend)
            {
                if (OnError != null)
                {
                    OnError(null, errMsg);
                }
            }
        }

    }


    IEnumerator DoServerCommunication(ServerState serverState, string url, Dictionary<string, string> data = null)//why is = operator present in one of the parameters?
    {
        CommunicatingServer = true;//declaring that a communication coroutine is ongoing

        WWWForm serverForm = new WWWForm();
        if (data != null && data.Count > 0)//checks if not sending empty data
        {
            foreach (KeyValuePair<string, string> dataInner in data)
            {
                serverForm.AddField(dataInner.Key.ToString(), dataInner.Value.ToString());
            }
        }

        UnityWebRequest www = new UnityWebRequest(url);//creates a web connection to the server given the url

        if (data != null && data.Count > 0)//if data empty, asks for data
        {
            www = UnityWebRequest.Post(url, serverForm);//sending data
        }
        else//else asks for data
        {
            www = UnityWebRequest.Get(url);//collecting data
        }

        if (!String.IsNullOrEmpty(token))
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);
        }

        yield return www.SendWebRequest();//waits until web request have finished
        CommunicatingServer = false;//declares and end to communication

        //after communicating with the server the err checking and handling starts or handling the receved data

        string resultString = www.downloadHandler.text;
        string errMsg = string.Empty;
        bool errHappend = false;

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("server comm err");
            Debug.Log($"error: {www.error}");
            Debug.Log($"response {resultString}");

            if (serverState == ServerState.logIn)
            {
                if (OnLoggedIn != null)
                {
                    OnLoggedIn(false);//declares the user in not logged in
                }
            }

            if (serverState == ServerState.auth)
            {
                if (OnAuth != null)
                {
                    OnAuth(false);//declares the user in not logged in
                }
            }

            if (serverState == ServerState.getRequest)
            {
                if (OnGetRequest != null)
                {
                    OnGetRequest(false, null);
                }
            }

            if (serverState == ServerState.sendRequest)
            {
                if (OnSentRequest != null)
                {
                    OnSentRequest(false, null);
                }
            }

        }
        else
        {
            Debug.Log($"response .. {resultString}");
            Text = resultString;
            if (serverState == ServerState.logIn)//here it checkes the log in data
            {
                LogInResponse logInResponse = new LogInResponse();//login responce is a data container class used to store json string packet from server

                try
                {
                    logInResponse = JsonUtility.FromJson<LogInResponse>(resultString);//tryes to read json string and make it into data for the build
                }
                catch
                {
                    logInResponse.code = "jwt_auth_failed";
                }

                if (!String.IsNullOrEmpty(logInResponse.token))//if token key is not empty
                {
                    if (OnLoggedIn != null)
                    {
                        OnLoggedIn(true);
                    }
                    token = logInResponse.token;//sets the local variable for token to be used for sending data
                }
                else//else it will make login failed
                {
                    if (OnLoggedIn != null)
                    {
                        OnLoggedIn(false);
                    }
                    errMsg = logInResponse.message;
                    errHappend = true;
                }
            }

            if (serverState == ServerState.auth)//here it checkes the log in data
            {
                AuthResponse authResponse = new AuthResponse();//login responce is a data container class used to store json string packet from server

                try
                {
                    authResponse = JsonUtility.FromJson<AuthResponse>(resultString);//tryes to read json string and make it into data for the build
                    Debug.Log("auth resp success" + authResponse.description);

                }
                catch
                {
                    Debug.Log("auth resp err " + authResponse.description);
                    authResponse.status = -1;
                }

                if (authResponse.status != -1)//if token key is not empty
                {
                    if (OnAuth != null)
                    {
                        OnAuth(true);
                    }
                }
                else//else it will make login failed
                {
                    if (OnAuth != null)
                    {
                        OnAuth(false);
                    }
                    errMsg = authResponse.description;
                    errHappend = true;
                }
            }


            if (serverState == ServerState.getRequest)
            {
                GetResponse get = new GetResponse();

                try
                {
                    get = JsonUtility.FromJson<GetResponse>(resultString);
                }
                catch
                {
                    get.status =-1;
                }

                if (OnGetRequest != null)
                {
                    if (String.IsNullOrEmpty(get.data.container3))
                    {
                        OnGetRequest(false, null);

                    }
                    else
                    {
                        OnGetRequest(true, get.data.container3);
                    }
                }
            }
            if (serverState == ServerState.sendRequest)
            {
                SendResponse set = new SendResponse();

                try
                {
                    set = JsonUtility.FromJson<SendResponse>(resultString);
                }
                catch
                {
                    set.status = -1;
                }

                if (OnSentRequest != null)
                {
                    if (set.status == -1)
                    {
                        OnSentRequest(false, null);

                    }
                    else
                    {
                        OnSentRequest(true, set.data.container3);
                    }
                }
            }

            if (errHappend)
            {
                if (OnError != null)
                {
                    OnError(null, errMsg);
                }
            }
        }
    }

}