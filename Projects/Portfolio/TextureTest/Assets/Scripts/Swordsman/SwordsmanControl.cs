using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanControl : Controller
{
    [SerializeField] Collider2D DetectionArea;
    GameObject target;
    private enum State{patrol, fighting, idle}
    //todo set up bot AI
    public override void ExecuteMovement()//here goes movement
    {
        motor.Peek().MoveHorizontal(Vector2.zero);
    }
    public override void ExecuteControlls()//here go attack/abilities
    {
        motor.Peek().Animate();
    }

   

}
