using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanDeff : BotDeff 
{
    private void Start()
    {
        controller.Push(GetComponent<Controller>()); //initialises bot controll
        abilities = GetComponent<Abilities>();
        alive = true;
        possesd = false;
        energyRegen = durability = 200;
    }
    private void FixedUpdate()
    {
        if(!possesd)//will stop controlling it self if possesd
            controller.Peek().ExecuteMovement();
    }

}
