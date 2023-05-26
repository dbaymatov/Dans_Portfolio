using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInstructions : MonoBehaviour
{
    [SerializeField] GameObject InstructionsPanel;
    [SerializeField] List<GameObject> Instructions;
    [SerializeField] GameObject MainMenu;
    int currentInstruction;
    // Start is called before the first frame update
    void Start()
    {
        currentInstruction = 0;
    }

    public void ActivateNextInstruction()
    {
        Instructions[currentInstruction].SetActive(false);

        if (currentInstruction + 1 == Instructions.Count)
        {
            InstructionsPanel.SetActive(false);
            MainMenu.SetActive(true);
            currentInstruction = 0;
            //here it starts runing the game
        }

        else
        {
            Instructions[currentInstruction + 1].SetActive(true);
            currentInstruction++;
        }

    }
}
