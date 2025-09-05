using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : Controller
{
    //todo set up bot AI
 public override void ExecuteMovement()//here goes movement
    {
        motor.Peek().MoveHorizontal(Vector2.zero);//makes it stop moving
    }
    public override void ExecuteControlls()//here go attack/abilities
    {
        motor.Peek().Animate();
    }



}
