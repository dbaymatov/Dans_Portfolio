using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoevementManagerScene2 : MonoBehaviour

{
    //player selection variables
    [SerializeField] Animator PantherAnimator;
    [SerializeField] Animator BirdAnimator;
    [SerializeField] GameObject Panther;
    [SerializeField] GameObject Bird;
    Animator animator;

    //waypoints, datamanagers and effects variables
    [SerializeField] List<MovementData> movePointsL;
    [SerializeField] List<MovementData> movePointsR;
    [SerializeField] Scene2DataManager dm;
    [SerializeField] TextRise tr;
    [SerializeField] LavaRise lr;
    [SerializeField] BackdropManager background;
    [SerializeField] Level2EndgameContinue end;
    List<MovementData> chosenPath;

    int nextPoint;
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {

        //character initialization from data manager file read
        //InitialiseCharacter(dm.selectedCharacter);

        EventManager.TimeOutBroadcast.AddListener(TimeOut);
        animator.Play("Idle");
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            MoveTowards();
        }
    }

    public void MoveTowards()
    {
        transform.eulerAngles = chosenPath[nextPoint].rotations;

        switch (chosenPath[nextPoint].selectedMovement)
        {
            case MovementData.movementType.teleport:
                //teleport
                transform.position = chosenPath[nextPoint].targetLocationn.transform.position;
                break;
            case MovementData.movementType.translate:
                //translation over time
                transform.position = transform.position + (Vector3.Normalize(chosenPath[nextPoint].targetLocationn.transform.position - transform.position)) * chosenPath[nextPoint].speed * Time.deltaTime;

                //snaps player to location if it gets close enough
                if (Vector3.Distance(transform.position, chosenPath[nextPoint].targetLocationn.transform.position) < 3)
                {
                    transform.position = chosenPath[nextPoint].targetLocationn.transform.position;

                }
                break;
        }

        //when triggered changes background and resets lava
        if (IsOnPoint() && chosenPath[nextPoint].changeSceneBackground && !dm.gameEnd)
        {
            background.PickRandomBackground();
            tr.ResetText();
            lr.ResetLava();
        }
        //when game ends will enable the end game background
        else if (IsOnPoint() && chosenPath[nextPoint].changeSceneBackground && dm.gameEnd)
        {
            background.Disable();
            background.EnableEndGame();
            tr.ResetText();
            lr.ResetLava();

            Debug.Log("Game end detected");
        }


        //if there still points to visit and it reache current destination will move on to the next one
        if (nextPoint < chosenPath.Count - 1 && IsOnPoint())
        {

            //if question should activate on that point the player will stop at it and playe idle animation while activating the interface
            if (chosenPath[nextPoint].questionActivate)
            {
                dm.GameOn();
                animator.Play("Idle");
                moving = false;
            }
            //else it will continue on to the next point
            else
            {
                nextPoint++;
                PlayAnimation();

            }
            //once reaches point it sets the next one as destination
        }

        //on last point does not turn on interface
        else if (IsOnPoint()&&!dm.gameEnd)
        {
            dm.GameOn();
            moving = false;
            nextPoint = 0;
            //stops all movement and resets the waypoint list;
        }

        //when game ends stops its movement and lets other script handle last movements
        else if (IsOnPoint() && dm.gameEnd)
        {
            moving = false;
            nextPoint = 0;
            end.ShowPanel();
            //stops all movement and activates endgame script;
        }

    }

    //playes animation base on point destination
    public void PlayAnimation()
    {
        animator.Play(chosenPath[nextPoint].animationPlay);
    }

    public bool IsOnPoint()
    {
        if (transform.position == chosenPath[nextPoint].targetLocationn.transform.position)
            return true;
        return false;
    }

    //tells pick left or right path to pick
    public void ChoosePathAndPlay(bool right)
    {
        if (right)
        {
            chosenPath = movePointsR;
        }
        else
        {
            chosenPath = movePointsL;
        }
        moving = true;
    }

    public void ButtonPress(int left)
    {
        //cenverting int to bool and setting appropriate direction
        if (left == 0)
        {
            ChoosePathAndPlay(false);
            Debug.Log("Going left");
        }
        else if (left == 1)
        {
            Debug.Log("Going right");
            ChoosePathAndPlay(true);
        }

        nextPoint = 0;
        PlayAnimation();
    }
    public void TimeOut()
    {
        ChoosePathAndPlay(true);
        nextPoint = 0;
        PlayAnimation();
        dm.GameOff();
    }


    //will set character animator depending on the loadfile for both scripts (this one and the endgame)
    public void InitialiseCharacter(int character)
    {
        Debug.Log("Character int " + character);
        if (character == 0)
        {
            animator= BirdAnimator;
            Bird.SetActive(true);
            Panther.SetActive(false);
            end.animator = BirdAnimator;
        }
        else
        {
            animator = PantherAnimator;
            Bird.SetActive(false);
            Panther.SetActive(true);
            end.animator = PantherAnimator;

        }

    }

}

