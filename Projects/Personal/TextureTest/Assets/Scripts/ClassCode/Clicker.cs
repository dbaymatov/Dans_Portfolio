using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clicker : MonoBehaviour
{
    [SerializeField] Text clickedNumberDisp;
    [SerializeField] Text messegeDisp;
    [SerializeField] InputField playerInput;

    [SerializeField] Button clickerButton;

    public int clickCounter;

    float timer;

    enum gameState
    {
        waiting,
        begin,
        gameOn
    }

    gameState currentState;

    float waitTimer;

    void Start()
    {
        currentState = gameState.waiting;
        clickCounter = 0;
    }

    void Update()
    {
        clickedNumberDisp.text = clickCounter.ToString();

        switch (currentState)
        {
            case gameState.waiting:
                messegeDisp.text = "Eneter name and press Begin to Start";

                break;
            case gameState.begin:
                Begin();
                break;
            case gameState.gameOn:
                GameOn();
                break;
            default:
                break;
        }

    }

    public void OnClickAdd()
    {

        if (currentState == gameState.gameOn)
        {
            clickCounter++;

        }

    }

    public void ClickToStart()
    {

        if (playerInput.text!=string.Empty) {

            waitTimer = 3;
            timer = 10;
            currentState = gameState.begin;
            clickCounter = 0;
        }
    }

    void Begin()
    {
        waitTimer -= Time.deltaTime;

        messegeDisp.text = waitTimer.ToString();

        if (waitTimer <= 0)
        {
            currentState = gameState.gameOn;

            Button buttonColor = clickerButton.GetComponent<Button>();
            ColorBlock colors = buttonColor.colors;
            colors.normalColor = Color.green;
            colors.highlightedColor = Color.green;
            colors.selectedColor = Color.green;

            buttonColor.colors = colors;


        }

    }

    void GameOn()
    {
        

        timer -= Time.deltaTime;

        messegeDisp.text = timer.ToString();


        if (timer <= 0)
        {

            currentState = gameState.waiting;

            Button buttonColor = clickerButton.GetComponent<Button>();
            ColorBlock colors = buttonColor.colors;
            colors.normalColor = Color.red;
            colors.highlightedColor = Color.red;
            colors.selectedColor = Color.red;
            buttonColor.colors = colors;

        }

    }

}
