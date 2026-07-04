using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class BirdMotor : Motor

{
    [SerializeField] float accelerationV;
    [SerializeField] float accelerationH;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float deadjumpForce;
    [SerializeField] float deadAccelerationV;
    [SerializeField] float deadAccelerationH;
    [SerializeField] float deadMaxSpeed;

    //makes bird flap its wing causing it to move up
    public override void MoveVertical(Vector2 move)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector2(0, move.y * Time.deltaTime * accelerationV));
        }
    }

    //makes bird fly and walk on ground, holding W,S will make glide or dive
    public override void MoveHorizontal(Vector2 move)
    {
        horizontalInput = move.x;
        if (!IsGrounded())//when in flight will move max speed
        {
            if (rb.velocity.x < maxSpeed && move.x > 0)
            {
                rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationH, 0));
            }
            if (rb.velocity.x > -maxSpeed && move.x < 0)
            {
                rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationH, 0));
            }
        }

        else//when moving on ground will move 1/4 of the speed
        {
            if (rb.velocity.magnitude < maxSpeed / 10)
            {
                rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationH, 0));
            }
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
        anim.SetTrigger("Flap");
        rb.AddForce(new Vector2(0, jumpForce));
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
            anim.SetBool("Flying", true);
        }
        else
        {
            anim.SetBool("Flying", false);
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