using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SpiritMotor : BotMotor
{
    public override void Move(Vector2 movementVector)
    {
        rb.AddForce(movementVector * Time.deltaTime * speed);
        MoveAnim();
    }
}
