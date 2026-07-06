using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectResults : MonoBehaviour
{
    [SerializeField] GameObject CongratsScreen, ResultsScreen;
    // Start is called before the first frame update
    public void ShowResults()
    {
        CongratsScreen.gameObject.SetActive(false);

        ResultsScreen.gameObject.SetActive(true);
    }
}
