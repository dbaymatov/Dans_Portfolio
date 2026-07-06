using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IslandTracker : MonoBehaviour
{
    [SerializeField] List<GameObject> IslandButons;
    public SaveFile container;//island map tutorial will initialize this variable
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < container.islandDataList.Count-1; i++)//one of the islands containers is used to store data for results screen which is not reperesned by one of the buttons thus -1.
        {
            if (container.islandDataList[i].complete)
            {
                GameObject text = IslandButons[i].transform.GetChild(0).gameObject;
                text.SetActive(true);
                Debug.Log("found component in child" + text);
                Button button = IslandButons[i].GetComponent<Button>();
                Image img = IslandButons[i].GetComponent<Image>();
                img.color = Color.blue;
                button.interactable = false;
                Debug.Log("forloop counted i is " + i);

            }
        }
    }
}
