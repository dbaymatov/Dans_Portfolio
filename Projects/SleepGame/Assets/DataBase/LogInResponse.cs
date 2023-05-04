using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LogInResponse
{
    public string token;
    public string user_display_name;
    public string user_email;
    public string user_nicename;

    public string code;
    public Data data;
    public string message;
}

