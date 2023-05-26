using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoevementManagerScene3 : MonoBehaviour

{

    [SerializeField] Animator PantherAnimator;
    [SerializeField] Animator BirdAnimator;
    [SerializeField] GameObject Panther;
    [SerializeField] GameObject Bird;


    Animator animator;
    [SerializeField] List<MovementData> movePointsL;
    [SerializeField] List<MovementData> movePointsR;
    [SerializeField] Scene3DataManager dm;
    [SerializeField] BackdropManager background;
    [SerializeField] PlatformDestroy pf;
    [SerializeField] Level3EndGame end;
    List<MovementData> chosenPath;
    public bool gameEnd;

    int selectedCharacter;
    int nextPoint;
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        gameEnd = false;
        //InitialiseCharacter(dm.character);

        EventManager.TimeOutBroadcast.AddListener(ButtonPress2);
        animator.Play("Idle");
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
            default:
                break;
        }

        if (IsOnPoint() && chosenPath[nextPoint].changeSceneBackground && !dm.gameEnd)
        {
            background.PickRandomBackground();
            Debug.Log("calling reset");
            pf.ResetRoom();
        }
        //when game ends will enable the end game background
        else if (IsOnPoint() && chosenPath[nextPoint].changeSceneBackground && dm.gameEnd)
        {
            background.Disable();
            background.EnableEndGame();
            pf.ResetRoom();
            end.ShowPanel();
            Debug.Log("Game end detected");
        }



        //if there still points to visit and it reache current destination will move on to the next one
        if (nextPoint < chosenPath.Count - 1 && IsOnPoint())
        {
            if (chosenPath[nextPoint].questionActivate)
            {
                dm.GameOn();
                animator.Play("Idle");
                moving = false;
            }
            else
            {
                nextPoint++;
                PlayAnimation();

            }
            //once reaches point it sets the next one as destination

        }

        //on last point does not turn on interface
        else if (IsOnPoint() && !dm.gameEnd)
        {
            dm.GameOn();
            moving = false;
            nextPoint = 0;
            //stops all movement and resets the waypoint list;
        }
        else if (IsOnPoint() && dm.gameEnd)
        {
            moving = false;
            nextPoint = 0;
            //stops all movement and resets the waypoint list;
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
        if (left == 1)
        {
            ChoosePathAndPlay(false);
            Debug.Log("Going left");
        }
        else if (left == 2)
        {
            Debug.Log("Going right");
            ChoosePathAndPlay(true);
        }

        nextPoint = 0;
        moving = true;
        PlayAnimation();
    }
    public void ButtonPress2()
    {
        ChoosePathAndPlay(false);
        moving = true;
        PlayAnimation();
        nextPoint = 0;

    }

    public void TimeOut()
    {
        ChoosePathAndPlay(true);
        nextPoint = 0;
        PlayAnimation();
        dm.GameOff();
    }

    public void InitialiseCharacter(int character)
    {

        if (character == 0)
        {
            animator = BirdAnimator;
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

