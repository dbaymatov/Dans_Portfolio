using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{
    [SerializeField] PlayScript ps;
    [SerializeField] List<Text> valueNames;//no longer used
    [SerializeField] List<Text> valueScore;

    [SerializeField] Text FamilyScore;
    [SerializeField] Text LifeScore;
    [SerializeField] Text RecScore;
    [SerializeField] Text ContribScore;
    [SerializeField] Text FinanceScore;
    [SerializeField] Text HealthScore;
    [SerializeField] Text SocialCredScore;
    [SerializeField] Text WorkScore;


    List<string> resultNames = new List<string>();
    List<float> score = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        LoadSetData();
        //GetData();
        //SortData();
        //SetData();
    }
    void GetData()
    {
        for (int i = 0; i < ps.myContainer.v.Count;)
        {
            resultNames.Add(ps.myContainer.v[i].valueName);
            score.Add(ps.myContainer.v[i].valueRating);
            i++;
        }
    }
    void SortData()
    {
        for (int i = 0; i < score.Count;)
        {
            float temp1 = score[i];
            string temp2 = resultNames[i];

            for (int j = i; j < score.Count;)
            {
                if (score[i]>score[j])
                {
                    temp1 = score[i];
                    score[i] = score[j];
                    score[j] = temp1;

                    temp2 = resultNames[i];
                    resultNames[i] = resultNames[j];
                    resultNames[j] = temp2;
                }
                j++;
            }
            i++;
        }
    }
    void SetData()
    {
        for (int i = 0; i < ps.myContainer.v.Count;)
        {
            valueNames[i].text = resultNames[i];
            valueScore[i].text = score[i].ToString();
            i++;
        }
    }
    void LoadSetData()
    {
        FamilyScore.text = GetScore("Family");
        LifeScore.text = GetScore("Life Purpose");
        RecScore.text = GetScore("Recreation");
        ContribScore.text = GetScore("Contribution");
        FinanceScore.text = GetScore("Finance");
        HealthScore.text = GetScore("Health");
        SocialCredScore.text = GetScore("Social Life");
        WorkScore.text = GetScore("Work");
    }
    string GetScore(string name)
    {
        for (int i = 0; i < ps.myContainer.v.Count;)
        {
            if (name == ps.myContainer.v[i].valueName)
            {
                return ps.myContainer.v[i].valueRating.ToString();
            }


            i++;
        }
        return "0";
    }
}
