using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class RatMotor : Motor

{
    [SerializeField] float accelerationV;
    [SerializeField] float accelerationH;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float deadjumpForce;
    [SerializeField] float deadAccelerationH;
    [SerializeField] float deadAccelerationV;
    [SerializeField] float deadMaxSpeed;
    //rat should not be able to move vertically, placeholder method
    public override void MoveVertical(Vector2 move)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector2(0, move.y * Time.deltaTime * accelerationV));
        }
    }

    //makes rat walk on ground, 
    public override void MoveHorizontal(Vector2 move)
    {
        horizontalInput = move.x;
        if (rb.velocity.x < maxSpeed && move.x > 0)
        {
            rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationH, 0));
        }
        if (rb.velocity.x > -maxSpeed && move.x < 0)
        {
            rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationH, 0));
        }
        if (move.x != 0)
        {
            ScaleVertical(Math.Sign(move.x));//scales character by 1 or -1 depending on which direction it is going
        }
        KillMomentum();
    }
    public override void SpecialMove()
    {
    }
    //adds force up 
    public override void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); //cancells vertical speed
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
    //replaces used parameters with the ones from undead state
    public override void BecomeUndead()
    {
        jumpForce = deadjumpForce;
        accelerationV = deadAccelerationV;
        accelerationH = deadAccelerationH;
        maxSpeed = deadMaxSpeed;
    }
    public override void Animate()
    {
        if (horizontalInput != 0)//controls run/idle
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        if (!IsGrounded())//controlls fall animation
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }
    }
    //method used in the update function in bot deff inherited script
    public override void ExecuteMove(Vector2 move)
    {
        MoveHorizontal(move);
        MoveVertical(move);
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpecialMove();
        }
    }

}