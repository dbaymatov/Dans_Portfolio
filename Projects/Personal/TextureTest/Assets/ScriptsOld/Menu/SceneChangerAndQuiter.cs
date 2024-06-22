using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerAndQuiter : MonoBehaviour
{
    public void Play(int n)
    {

        SceneManager.LoadScene(n);
        Debug.Log("Scene Should be changed to " + n);
    }

    public void Activate(GameObject obj)
    {

        obj.SetActive(true);
    }
    public void Deactivate(GameObject obj)
    {

        obj.SetActive(false);
    }

    public void Exit()
    {

        Application.Quit();

    }
}
