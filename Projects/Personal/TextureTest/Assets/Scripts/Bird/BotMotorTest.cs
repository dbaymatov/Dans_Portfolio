using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class BotMotorTest : Motor

{
    [SerializeField] float accelerationHorizontal;
    [SerializeField] float accelerationVertical;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float deadjumpForce;
    [SerializeField] float deadAccelerationV;
    [SerializeField] float deadAccelerationH;

    [SerializeField] float deadMaxSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override void MoveHorizontal(Vector2 move)
    {
        // if (Math.Abs(rb.velocity.x) < maxSpeed)
        // {
            

        //     // if (IsGrounded(1f))
        //     // {
        //     //     rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationHorizontal, 0));
        //     // }
        //         rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationHorizontal, 0));

        // }

        if(rb.velocity.x<maxSpeed && move.x>0){

                rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationHorizontal, 0));

        }
        if(rb.velocity.x>-maxSpeed && move.x<0){
                rb.AddForce(new Vector2(move.x * Time.deltaTime * accelerationHorizontal, 0));
        }
        if (move.x != 0)
            {
                //ChangeDirection(Math.Sign(move.x));//scales character by 1 or -1 depending on which direction it is going
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
    public override void Animate()
    {
    }
    public override void ExecuteMove(Vector2 move)//method passed to the bot deff class update function
    {
        MoveHorizontal(move);
        MoveVertical(move);
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpecialMove();
        }
    }

}