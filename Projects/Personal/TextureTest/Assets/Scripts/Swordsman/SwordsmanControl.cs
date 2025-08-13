using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanControl : Controller
{

    //todo set up bot AI
    public override void ExecuteMovement()//here goes movement
    {
        motor.Peek().Animate();
    }
    public override void ExecuteControlls()//here go attack/abilities
    {

    }

   

}
