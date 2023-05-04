using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;


public class CheckBoxController : MonoBehaviour
{
    public Toggle toggle;
    public InventoryItem item;
    public ResultsScreenManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ResultsScreenManager>();

        toggle = GetComponent<Toggle>();
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(delegate { OnValueChanged(); });

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnValueChanged()
    {
        if (!manager.CanSelectMore())
        {
            toggle.isOn = false;
        }
    }

}
