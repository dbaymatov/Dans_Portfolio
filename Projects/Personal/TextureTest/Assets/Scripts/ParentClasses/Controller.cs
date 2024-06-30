using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Controller : MonoBehaviour
{
    public Stack<Motor> motor = new Stack<Motor>();
    public Stack<Abilities> abilities = new Stack<Abilities>();

    public abstract void ExecuteMovement();
    public abstract void ExecuteControlls();
    //will change direction based on the player horizontal movement, will accept -1 +1 arguments representing left and right directions

    public void ChangeDirection(float direction){

        transform.localScale = new Vector2(direction,transform.localScale.y);
    }

}
