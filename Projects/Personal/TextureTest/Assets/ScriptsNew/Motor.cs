using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class Motor:MonoBehaviour
{
    public abstract void ExecuteMove(Vector2 move);
    public abstract void MoveVertical(Vector2 move);
    public abstract void MoveHorizontal(Vector2 move);
    public abstract void SpecialMove();
    public abstract void Jump();
    public bool IsGrounded(float distance)
    {
        Debug.Log(transform.name);
        //checks distance to the ground and returns bool if ground within the given distance
        return Physics2D.Raycast(transform.position, Vector2.down, distance);
       // return Physics2D.Raycast();

    }
}
