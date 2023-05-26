using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2EndgameContinue : MonoBehaviour
{
    [SerializeField] int waitTime;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] KeyLightDissappear key;
    public Animator animator;//this varaiable will be initialised by the movement manager
    [SerializeField] List<MovementData> chosenPath;
    [SerializeField] Scene2DataManager dm;
    [SerializeField] GameObject SceneLoader;
    bool moving;
    int nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        nextPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            MoveTowards();
        }
    }


    void MoveTowards()
    {
        transform.eulerAngles = chosenPath[nextPoint].rotations;

        //translation over time
        transform.position = transform.position + (Vector3.Normalize(chosenPath[nextPoint].targetLocationn.transform.position - transform.position)) * chosenPath[nextPoint].speed * Time.deltaTime;

        //snaps player to location if it gets close enough
        if (Vector3.Distance(transform.position, chosenPath[nextPoint].targetLocationn.transform.position) < 3)
        {
            transform.position = chosenPath[nextPoint].targetLocationn.transform.position;
        }

        //here will decide whether to stop since no more points left or continue to the next

        //if there still points to visit and it reaches current destination will move on to the next one
        if (nextPoint < chosenPath.Count - 1 && IsOnPoint())
        {
                nextPoint++;
                PlayAnimation();
        }
        //on last point does not turn on interface
        else if (IsOnPoint())
        {
            moving = false;
            nextPoint = 0;
            //stops all movement and resets the waypoint list;
        }

    }
    //playes animation base on point destination
    void PlayAnimation()
    {
        animator.Play(chosenPath[nextPoint].animationPlay);
    }

    bool IsOnPoint()
    {
        if (transform.position == chosenPath[nextPoint].targetLocationn.transform.position)
            return true;
        return false;
    }


    public void ShowPanel()
    {
        StartCoroutine(ShowResultsScreen());
        key.CorotineStarter();
    }
    IEnumerator ShowResultsScreen()
    {
        yield return new WaitForSeconds(waitTime);
        endGamePanel.SetActive(true);
    }

    //method used to continue button on level 2 endscreen
    //will trigger coroutine to send player to next level after wait time for animation finishes
    public void ContinueClick()
    {
        moving = true;
        Time.timeScale = 1;
        endGamePanel.SetActive(false);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(4);

        SceneLoader.SetActive(true);
        SceneManager.LoadScene("Game3");

    }



}
