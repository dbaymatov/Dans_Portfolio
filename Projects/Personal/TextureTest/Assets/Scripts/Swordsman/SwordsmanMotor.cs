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
    [SerializeField] float groundBoxXsize;
    [SerializeField] float groundBoxYsize;
    [SerializeField] float groundBoxYpos;
    [SerializeField] float jumpForce;
    [SerializeField] float deadjumpForce;
    [SerializeField] float deadAccelerationV;
    [SerializeField] float deadAccelerationH;
    [SerializeField] float deadMaxSpeed;
    [SerializeField] float decelerationRate;
    [SerializeField] float moveSensetivity;
    [SerializeField] GameObject attackPoint;
    [SerializeField] Animator anim;

    Rigidbody2D rb;

    private float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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

        if (move.x == 0 && IsGrounded())
        {
            Vector2 currentVelocity = rb.velocity;
            currentVelocity.x = Mathf.Lerp(currentVelocity.x, 0f, Time.fixedDeltaTime * decelerationRate);
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
    public bool IsGrounded()//checks if the player is colliding with groind
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(groundBoxXsize, groundBoxYsize), 0, -transform.up, groundBoxYpos))
            return true;
        return false;
    }

    // private bool CheckIfMoving()
    // {
    //     if (rb.velocity.magnitude > moveSensetivity)
    //         return true;
    //     return false;
    // }
    private bool CheckIfFalling()
    {
        if (IsGrounded())
            return false;
        return true;
    }
    private void OnDrawGizmos()//draws ground checking box
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundBoxYpos, new Vector2(groundBoxXsize, groundBoxYsize));
    }

    public override void ExecuteMove(Vector2 move)//method passed to the bot deff class update function
    {
        MoveHorizontal(move);
        MoveVertical(move);
        //moving = CheckIfMoving();
        falling = CheckIfFalling();
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpecialMove();
        }
        Animate();
    }
    public override void Animate()
    {
        Debug.Log("horizontal input: " + horizontalInput);
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

    }

}