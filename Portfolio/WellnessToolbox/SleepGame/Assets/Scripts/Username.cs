using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;
using UnityEngine.UI;
using TMPro;

public class Username : MonoBehaviour
{
    SaveFile saveContainer = new SaveFile();
    [SerializeField] GameObject instructions, field, panel;
    public void SaveUsername()
    {
        
        if (File.Exists("SaveFiles.xml"))
        {
            //will try to open a file and check if its not corrupted or null
            Stream stream = File.Open("SaveFiles.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            saveContainer = (SaveFile)serializer.Deserialize(stream);
            stream.Close();
            Debug.Log("one");
            string name = field.GetComponent<TMP_InputField>().text;
            saveContainer.username = name;
            Debug.Log("attempting to save data");
            stream = File.Open("SaveFiles.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(SaveFile));
            serializer.Serialize(stream, saveContainer);
            stream.Close();

        }
        else
        {
            saveContainer = null;
            Stream stream = File.Open("SaveFiles.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            serializer.Serialize(stream, saveContainer);
            stream.Close();
            Debug.Log("two");
            string name = field.GetComponent<TMP_InputField>().text;
            saveContainer.username = name;
            Debug.Log("attempting to save data");
            stream = File.Open("SaveFiles.xml", FileMode.Create);
            serializer = new XmlSerializer(typeof(SaveFile));
            serializer.Serialize(stream, saveContainer);
            stream.Close();
        }
        instructions.SetActive(true);
        panel.SetActive(false);

    }
}
