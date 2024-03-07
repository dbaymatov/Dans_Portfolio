using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDeffTest : BotDeff 
{
    private void Start()
    {
        //controller = new Stack<Controller>();
        controller.Push(GetComponent<Controller>()); //initialises bot controll
        abilities = GetComponent<Abilities>();
        alive = true;
        possesd = false;
        hp = energyRegen = durability = 100;
    }
    private void Update()
    {
        if(!possesd)//will stop controlling it self if possesd
            controller.Peek().TakeAction();
    }

    public override void Posses(Controller playerController)
    {
        controller.Push(GetComponent<Controller>());//hijacks the controll of bot
        possesd=true;
    }
}
