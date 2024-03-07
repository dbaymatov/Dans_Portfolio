using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : Controller
{
    //todo set up bot AI
    public override void TakeAction()
    {
    }
    public override void ExecuteControlls()
    {
    }
    public override void AddMotor(Motor motor)//posses movement of mob
    {
        this.motor.Push(motor);
    }
    public override void RemoveMotor()//give up movement of mob
    {
        if (this.motor.Count > 1)
        {
            this.motor.Pop();
        }
    }
    public override void AddAbility(Abilities ability)//posses movement of mob
    {
        this.abilities.Push(ability);
    }
    public override void RemoveAbility()//give up movement of mob
    {
        if (this.abilities.Count > 1)
        {
            this.abilities.Pop();
        }
    }


}
