using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answers 
{
    public string smart;
    public Answers()
    {
        smart = "pizza";
    }

    public string getValue()
    {
        return smart;
    }

    public void setValue(string s)
    {
        smart = s;
    }
}
