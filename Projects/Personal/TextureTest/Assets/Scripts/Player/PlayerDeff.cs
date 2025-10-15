using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
public class PlayerDeff : BotDeff
{
    public Image HealthBar;
    private void Start()
    {
        controller.Push(GetComponent<Controller>()); //player controll
        alive = true;
        possesd = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        controller.Peek().ExecuteMovement();//controlls movement

    }
    private void Update()
    {
        //Debug.Log("player update");
        controller.Peek().ExecuteControlls();//controlls of the abilities
        RegenEnergy();

        //if (!controller.Peek().possesing)
          //  RegenEnergy();
        if (currentEnergy < 0)
        {
            Debug.Log("Game Over");
        }

    }
    public override void RegenEnergy()//for now moved energy degeneration to EnergyManager for player
    {
        //currentEnergy += energyRegen * Time.deltaTime;
        //if (currentEnergy > maxEnergy)//in case current energy exceeds the acceptable levels of energy
        //    currentEnergy = maxEnergy;
    }


}
