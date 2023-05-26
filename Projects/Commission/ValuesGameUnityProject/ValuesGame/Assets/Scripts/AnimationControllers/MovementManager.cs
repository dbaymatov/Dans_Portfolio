using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script loads the chosen player character from the file and controls its movement

public class MovementManager : MonoBehaviour
{
    //animator variables
    [SerializeField] Animator PantherAnimator;
    [SerializeField] Animator BirdAnimator;
    [SerializeField] GameObject Panther;
    [SerializeField] GameObject Bird;


    [SerializeField] List<MovementData> movePoints;
    [SerializeField] Scene1DataManager dm;
    [SerializeField] PlatformFall platformFall;
    List<MovementData> chosenPath;

    private int activeCharacter = 0;
    private Animator currentCharacter;

    int nextPoint;
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        activeCharacter = PlayerPrefs.GetInt("selectedCharacter");
        if (activeCharacter == 0)
        {
            currentCharacter = BirdAnimator;
            Bird.SetActive(true);
            Panther.SetActive(false);
        }
        else
        {
            currentCharacter = PantherAnimator;
            Bird.SetActive(false);
            Panther.SetActive(true);
        }

        EventManager.TimeOutBroadcast.AddListener(TimeOut);
        currentCharacter.Play("Idle");

        moveToNextPoint();

        moving = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            MoveTowards();
        }
    }

    public void MoveTowards()
    {
        //here all the actual movement is happening
        switch (chosenPath[nextPoint].selectedMovement)
        {
            //teleport
            case MovementData.movementType.teleport:
                transform.position = chosenPath[nextPoint].targetLocationn.transform.position;
                break;
            //translation over time
            case MovementData.movementType.translate:
                transform.position = transform.position + (Vector3.Normalize(chosenPath[nextPoint].targetLocationn.transform.position - transform.position)) * chosenPath[nextPoint].speed * Time.deltaTime;
                //snaps player to location if it gets close enough
                if (Vector3.Distance(transform.position, chosenPath[nextPoint].targetLocationn.transform.position) < 3f)
                {
                    transform.position = chosenPath[nextPoint].targetLocationn.transform.position;
                }
                break;
        }

        //if there still points to visit and it reache current destination will move on to the next one
        if (nextPoint < chosenPath.Count - 1 && IsOnPoint())
        {
            if (chosenPath[nextPoint].questionActivate)
            {
                // here it activates the question panel and starts platform shake
                dm.GameOn();
                currentCharacter.Play("Idle");
                moving = false;
                transform.parent = platformFall.platforms[platformFall.currentPlatform].transform;
                platformFall.FallPlatform();
            }



            else
            {
                nextPoint++;
                PlayAnimation();
            }
            //once reaches point it sets the next one as destination
            if (nextPoint == 54)
            {
                currentCharacter.Play("Idle");
                moving = false;
                Debug.Log("I worked");
            }

        }

        //on last point does not turn on interface
        else if (IsOnPoint())
        {
            moving = false;
        }
    }

    //playes animation base on point destination
    public void PlayAnimation()
    {
        currentCharacter.Play(chosenPath[nextPoint].animationPlay);

    }

    public bool IsOnPoint()
    {
        if (transform.position == chosenPath[nextPoint].targetLocationn.transform.position)
            return true;
        return false;
    }

    //makes jump to another platform
    public void moveToNextPoint()
    {
        transform.parent = null;
        chosenPath = movePoints;
        moving = true;
    }

    public void ButtonPress()
    {
        nextPoint++;
        moveToNextPoint();
        moving = true;
        PlayAnimation();
    }

    //method triggered by event broadcast from the data manager scene 1
    public void TimeOut()
    {
        moveToNextPoint();

        if (chosenPath.Count - 2 > nextPoint)
        {
            nextPoint++;
        }
        moving = true;
        PlayAnimation();

    }
}


