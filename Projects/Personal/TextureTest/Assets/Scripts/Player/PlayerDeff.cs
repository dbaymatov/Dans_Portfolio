using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeff : BotDeff
{
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

        if(!controller.Peek().possesing) 
            RegenEnergy();
        else


        //Debug.Log(energyRegen + " Current energy"+ currentEnergy);
        if (currentEnergy < 0)
        {
            //Debug.Log("Game Over");
        }

    }


}
