using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveContainer
{
    public List<Answers> answers;
    public List<Values> v;
    public string ansName;
    
    public SaveContainer()
    {
        v = new List<Values>();
        answers = new List<Answers>();
        
    }
    public void AddValue()
    {
        v.Add(new Values());
    }

    public void AddAnswer()
    {
        answers.Add(new Answers());
    }
}
