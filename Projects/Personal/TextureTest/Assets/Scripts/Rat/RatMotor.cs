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
    [SerializeField] float groundBoxXsize;
    [SerializeField] float groundBoxYsize;
    [SerializeField] float groundBoxYpos;
    [SerializeField] float jumpForce;
    [SerializeField] float deadjumpForce;
    [SerializeField] float deadAccelerationH;
    [SerializeField] float deadAccelerationV;
    [SerializeField] float deadMaxSpeed;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
            //ChangeDirection(Math.Sign(move.x));//scales character by 1 or -1 depending on which direction it is going
        }

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

    
    //checks if there is collision mesh bellow
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(groundBoxXsize, groundBoxYsize), 0, -transform.up, groundBoxYpos))
            return true;
        return false;
    }
    //replaces used parameters with the ones from undead state
    public override void BecomeUndead()
    {
        jumpForce = deadjumpForce;
        accelerationV = deadAccelerationV;
        accelerationH = deadAccelerationH;
        maxSpeed = deadMaxSpeed;
    }
    //displayes grounded detection box 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundBoxYpos, new Vector2(groundBoxXsize, groundBoxYsize));
    }
    public override void Animate()
    {
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