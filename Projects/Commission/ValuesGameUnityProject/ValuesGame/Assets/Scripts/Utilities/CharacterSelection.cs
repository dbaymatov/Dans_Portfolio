using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public string nextScene;
    [SerializeField] GameObject loader;
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }
    public void StartGame()
    {
        Debug.Log($"Selected Characcter Value : {selectedCharacter}");
        loader.SetActive(true);
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        if(selectedCharacter == 1)
        {
            Debug.Log($"Panther Intro");
            SceneManager.LoadScene("Panther Intro");


            //   SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log($"Bird Intro");
            SceneManager.LoadScene("BirdIntro");

            //SceneManager.LoadScene(3);
        }

    }
}
