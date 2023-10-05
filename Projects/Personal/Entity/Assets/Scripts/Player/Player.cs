using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    public GhostMove moveInGhostState;
    public GameObject possesing;
    public float maxEnergy;
    private float ghostEnergy;

    [SerializeField] Text textEnergy;
    [SerializeField] private CircleCollider2D coll;

    [SerializeField] GameObject deathScreen;


    private Bot botData;
    private bool canPosses;

    private enum playerState
    {
        Ghost,
        Possesing,
        Dead,
    }
    private playerState currentState;

    void Start()
    {
        ghostEnergy = maxEnergy;
        currentState = playerState.Ghost;
        moveInGhostState.enabled = true;
        canPosses = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if player touches possesable bot it detects which bot it is and its data
        if (collision.gameObject.tag == "Posessable" && currentState == playerState.Ghost)
        {
            possesing = collision.gameObject;
            botData = possesing.GetComponent<Bot>();
            canPosses = true;

            botData.HighLight();//highlight the potential target

        }

        Debug.Log("I am touching" + collision.gameObject.name + " ");
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //if player stops touching possasable object it will loose option to posses it
        if (collision.gameObject.tag == "Posessable")
        {
            possesing = null;
            canPosses = false;
            botData.StopHighlight();//dehiglight the potential target

        }
    }


    void Update()
    {
        GhostEnrgyCaclulator();

        switch (currentState)
        {
            case playerState.Ghost:
                //whenever ghost is in its normal state it looses some of its energy
                Ghost();
                break;
            case playerState.Possesing:
                Possesing();
                break;
            case playerState.Dead:
                SceneManager.LoadScene(1);




                break;
            default:
                break;
        }

        //Debug.Log(canPosses);
    }

    private void Possesing()
    {
        transform.position = botData.transform.position;

        //if player posseses bot and presses x or bot fully dies it reactivates playaer default control and its collider, while also seting bot state to dead
        if (Input.GetKeyUp("x") || botData.currentState == Bot.botState.Dead)
        {
            transitionToGhost();
        }
    }


    //in ghost state every sec 1 energy is lost, and ghostmove scipt is active. 
    //if player howers on top of possesable mob it can press X to posses it,
    //thus changing ghost current state to possesing along with bots state.
    private void Ghost()
    {

        if (canPosses && Input.GetKeyUp("x"))
        {
            transitionToPossesing();

        }

    }

    void transitionToGhost()
    {
        moveInGhostState.enabled = true;
        botData.currentState = Bot.botState.Normal;
        coll.enabled = true;
        currentState = playerState.Ghost;

    }

    void transitionToPossesing()
    {
        coll.enabled = false;
        currentState = playerState.Possesing;
        moveInGhostState.enabled = false;
        botData.currentState = Bot.botState.Possesed;

    }

    private void GhostEnrgyCaclulator()
    {
        if (currentState == playerState.Possesing)
        {
            ghostEnergy += botData.energyProduction * Time.deltaTime;

            if (ghostEnergy >= maxEnergy)
                ghostEnergy = maxEnergy;

        }
        else
        {
            ghostEnergy -= 1 * Time.deltaTime;

        }


        if (ghostEnergy <= 0)
        {
            currentState = playerState.Dead;
        }


        textEnergy.text = "" + ghostEnergy;

    }

}
