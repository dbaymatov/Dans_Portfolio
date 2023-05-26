using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Instructions : MonoBehaviour
{
    // Usual stuff
    [SerializeField] GameObject InstructionsPanel;
    [SerializeField] List<GameObject> Instructions;
    [SerializeField] Scene1DataManager dm;
    [SerializeField] MovementManager mg;
    [SerializeField] int waitTime;
    [SerializeField] sliderMove move;
    [SerializeField] Level1InstructionsFeedback feedback;
    int currentInstruction;
    bool beginning;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTutorial());
        currentInstruction = 0;
        beginning = true;
    }

    //coroutine waites for the scene intro animation to finish and then start the tutorial
    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(waitTime); //waits for time to run out for intro to finish
        Debug.Log("instructions wait time over");
        yield return new WaitUntil(() => dm.showInstructions);//waits untill data loads
        InstructionsPanel.SetActive(true);
        feedback.ShowUIElement((L1_Instruction)(currentInstruction + 1));
    }

    public void ActivateNextInstruction()
    {
        Instructions[currentInstruction].SetActive(false);

        //here it starts runing the game when it reaches last intruction click
        if (currentInstruction + 1 == Instructions.Count)
        {
            InstructionsPanel.SetActive(false);
            Time.timeScale = 1;
            //Add Bool to only activate this code at start
            if (beginning)
            {
                mg.moving = true;
                mg.PlayAnimation();
            }
            beginning = false;
            move.OnceMore();
            currentInstruction = 0;//resets count in case another instruction is needed to happen.
        }

        //will continue on to the next instruction
        else
        {
            Instructions[currentInstruction + 1].SetActive(true);
            currentInstruction++;
        }

        // Visual Feedback for the instructions
        Debug.Log($"Show UI Element for: {Enum.GetName(typeof(L1_Instruction), (L1_Instruction)currentInstruction)}");
        feedback.ShowUIElement((L1_Instruction)(currentInstruction + 1));
    }
}
