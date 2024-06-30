using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public abstract class Motor : MonoBehaviour
{
    public abstract void ExecuteMove(Vector2 move);
    public abstract void MoveVertical(Vector2 move);
    public abstract void MoveHorizontal(Vector2 move);
    public abstract void SpecialMove();
    public abstract void Jump();
    public abstract void BecomeUndead();
    //will change direction based on the player horizontal movement, will accept -1 +1 arguments representing left and right directions
    public void ChangeDirection(float direction){

        transform.localScale = new Vector2(direction,transform.localScale.y);
    }

}