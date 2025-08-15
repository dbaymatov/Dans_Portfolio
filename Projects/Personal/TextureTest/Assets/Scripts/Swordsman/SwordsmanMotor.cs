using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordsmanMotor : Motor

{
    [SerializeField] float accelerationHorizontal;
    [SerializeField] float accelerationVertical;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float deadjumpForce;
    [SerializeField] float deadAccelerationV;
    [SerializeField] float deadAccelerationH;
    [SerializeField] float deadMaxSpeed;
    [SerializeField] float decelerationRate;
    [SerializeField] float moveSensetivity;
    [SerializeField] GameObject attackPoint;

    private float horizontalInput;


    public override void MoveHorizontal(Vector2 move)
    {
        horizontalInput = move.x;

        if (rb.velocity.x < maxSpeed && move.x > 0)
        {

            rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationHorizontal, 0));
            if (!lookingRight)
                FlipAttackPoint(attackPoint.transform);
        }
        if (rb.velocity.x > -maxSpeed && move.x < 0)
        {
            rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationHorizontal, 0));
            if (lookingRight)
                FlipAttackPoint(attackPoint.transform);
        }

        if (move.x == 0 && IsGrounded())//quickly slows down player when on ground
        {
            Vector2 currentVelocity = rb.velocity;
            currentVelocity.x = Mathf.Lerp(currentVelocity.x, 0f, Time.fixedDeltaTime * decelerationRate);
            //currentVelocity.y = Mathf.Lerp(currentVelocity.x, 0f, Time.fixedDeltaTime * decelerationRate);
            rb.velocity = currentVelocity;
        }

    }
    public override void MoveVertical(Vector2 move)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector2(0, move.y * Time.deltaTime * accelerationVertical));
        }
    }
    public override void SpecialMove()
    {
    }
    //checks if touches ground and adds force to rb if touches ground
    public override void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); //cancells vertical speed
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
    public override void BecomeUndead()//replaces defult alive values with undead values
    {
        jumpForce = deadjumpForce;
        accelerationHorizontal = deadAccelerationH;
        accelerationVertical = deadAccelerationV;

        maxSpeed = deadMaxSpeed;
    }
    private bool CheckIfFalling()
    {
        if (IsGrounded())
            return false;
        return true;
    }

    public override void ExecuteMove(Vector2 move)//method passed to the bot deff class fixed update function
    {
        MoveHorizontal(move);
        MoveVertical(move);
        //moving = CheckIfMoving();
        falling = CheckIfFalling();
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpecialMove();
        }
    }
    //Contains animation logic to play movement type animation based on the player input, speed of bot and grounded conditional, this method is passed to bot deff class update function
    public override void Animate()
    {
        //run and idle logic
        if (horizontalInput != 0 && IsGrounded())//playes run animation
        {
            anim.SetBool("Moving", true);
            if (lookingRight)
            {
                anim.SetFloat("X", 1);
            }

            else
            {
                anim.SetFloat("X", 0);
            }
        }
        else
        {
            anim.SetBool("Moving", false);//playes idle
        }

        //jump logic
        if (!IsGrounded())
        {
            anim.SetBool("Falling", true);
            if (lookingRight)
            {
                anim.SetFloat("X", 1);
            }
            else
            {
                anim.SetFloat("X", 0);
            }
            if (horizontalInput != 0)
            {
                anim.SetBool("Moving", true);//playes idle
            }
            else
            {
                anim.SetBool("Moving", false);//playes idle
            }
        }
        //landed exit jump state
        else
        {
            anim.SetBool("Falling", false);
            if (horizontalInput != 0)
            {
                anim.SetBool("Moving", true);//playes idle
            }
            else
            {
                anim.SetBool("Moving", false);//playes idle
            }
        }
        
    }

}