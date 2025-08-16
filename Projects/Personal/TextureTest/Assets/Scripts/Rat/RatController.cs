using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : Controller
{
    //todo set up bot AI
    public override void ExecuteMovement()//here goes movement
    {
        motor.Peek().MoveHorizontal(Vector2.zero);
        //motor.Peek().MoveHorizontal(new Vector2(0.02f,0)); //testing if it will return back controlls after possesion and before

    }
    public override void ExecuteControlls()//here go attack/abilities
    {
        motor.Peek().Animate();
    }



}
