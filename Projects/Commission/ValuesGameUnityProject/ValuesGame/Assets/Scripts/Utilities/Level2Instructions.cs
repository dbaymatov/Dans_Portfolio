using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Instructions : MonoBehaviour
{
    [SerializeField] GameObject InstructionsPanel;
    [SerializeField] List<GameObject> Instructions;
    [SerializeField] Scene2DataManager dm;
    [SerializeField] Level2InstructionsFeedback feedback;
    int currentInstruction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTutorial());
        currentInstruction = 0;
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(3);
        yield return new WaitUntil(() => dm.showInstructions);//waits untill data loads
        InstructionsPanel.SetActive(true);
        Debug.Log("show intrusctions");
        feedback.ShowUIElement((L2_Instruction)(currentInstruction));
    }

    public void ActivateNextInstruction()
    {
        Instructions[currentInstruction].SetActive(false);

        if (currentInstruction+1==Instructions.Count)
        {
            InstructionsPanel.SetActive(false);
            Time.timeScale = 1;
            currentInstruction = 0;//resets count in case another instruction is needed to happen.
            dm.GameOn();
            //here it starts runing the game
        }

        else
        {
            Instructions[currentInstruction + 1].SetActive(true);
            currentInstruction++;
        }
        
        Debug.Log($"Show UI Element for: {Enum.GetName(typeof(L2_Instruction), (L2_Instruction)currentInstruction+1)}");
        feedback.ShowUIElement((L2_Instruction)(currentInstruction+1));
    }
}
