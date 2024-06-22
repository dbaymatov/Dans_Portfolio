using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeff : BotDeff
{
    //bool possesing = false;

    private void Start()
    {
        controller.Push(GetComponent<Controller>()); //player controll
        alive = true;
        possesd = false;
        energyRegen = durability = 0;
        hp = 10;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        controller.Peek().ExecuteMovement();//controlls movement

    }
    private void Update()
    {
        controller.Peek().ExecuteControlls();//controlls of the abilities

    }
}
