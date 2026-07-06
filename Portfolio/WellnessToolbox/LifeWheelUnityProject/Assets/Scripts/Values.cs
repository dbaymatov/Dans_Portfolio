using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Values
{
    public string valueName;
    public float valueRating;
    public int valueLocID;

    public Values()
    {
        valueName = "jhjhjkhjkh";
        valueRating = 0;
        valueLocID = 0;
    }
    
    public Values(string n, float f, int i)
    {
        valueName = n;
        valueRating = f;
        valueLocID = i;
    }

    //Get the values related to the Values
    public string getName()
    {
        return valueName;
    }

    public float getRating()
    {
        return valueRating;
    }

    public int getID()
    {
        return valueLocID;
    }


    //Create each value related to the values
    public void changeRating(float rating)
    {
        valueRating = rating;
    }

    public void changeName(string name)
    {
        valueName = name;
    }

    public void changeID(int id)
    {
        valueLocID = id;
    }
    
}
