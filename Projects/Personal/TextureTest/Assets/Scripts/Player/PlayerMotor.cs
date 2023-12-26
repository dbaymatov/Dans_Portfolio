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

}
