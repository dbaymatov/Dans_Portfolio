using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTutotorail : MonoBehaviour
{
    int currentInstruction = 0;
    [SerializeField] List<GameObject> Instructions;
    [SerializeField] GameObject InstructionsPanel;

    // Update is called once per frame


    public void ActivateNextInstruction()
    {
        Instructions[currentInstruction].SetActive(false);
        //here it starts runing the game when it reaches last intruction click
        if (currentInstruction + 1 >= Instructions.Count)
        {
            InstructionsPanel.SetActive(false);
        }

        else
        {
            Instructions[currentInstruction + 1].SetActive(true);
            currentInstruction++;
        }
    }
}
