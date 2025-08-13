using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : Motor
{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;

    [SerializeField] Rigidbody2D rb;

    public override void MoveVertical(Vector2 move)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector2(move.x * Time.deltaTime * acceleration, 0));
        }
    }
    public override void MoveHorizontal(Vector2 move)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector2(0, move.y * Time.deltaTime * acceleration));
        }
    }

    public override void ExecuteMove(Vector2 move)
    {
        MoveHorizontal(move);
        MoveVertical(move);
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpecialMove();
        }
    }
    //unused methods
    public override void SpecialMove()
    {
    }
    public override void Jump()
    {
    }
    public override void BecomeUndead()
    {
    }
    public override void Animate()
    {
    }
}
