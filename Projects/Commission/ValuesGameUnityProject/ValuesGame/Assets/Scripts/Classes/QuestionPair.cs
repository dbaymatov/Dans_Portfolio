using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionPair
{
    public ValueClass Value1;
    public ValueClass Value2;
    public int choice;

    public QuestionPair(ValueClass value1, ValueClass value2)
    {
        choice = 0;//might be more efficient way to store data
        Value1 = value1;
        Value2 = value2;
    }

}