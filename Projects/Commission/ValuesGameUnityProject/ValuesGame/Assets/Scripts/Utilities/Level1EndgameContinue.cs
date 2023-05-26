using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level1EndgameContinue : MonoBehaviour
{
    [SerializeField] MovementManager mv;
    [SerializeField]int waitTime;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] Scene1DataManager dm;
    [SerializeField] GameObject SceneLoader;
    public void ContinueClick()
    {
        mv.moving = true;
        Time.timeScale = 1;
        endGamePanel.SetActive(false);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneLoader.SetActive(true);
        SceneManager.LoadScene("Game2");
    }
}
