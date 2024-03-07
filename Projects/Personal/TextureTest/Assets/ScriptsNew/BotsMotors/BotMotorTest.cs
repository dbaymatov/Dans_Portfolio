using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BotMotorTest : Motor

{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
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
    public override void SpecialMove()
    {
    }
    //checks if touches ground and adds force to rb if touches ground
    public override void Jump()
    {
        Debug.Log("jump method");
        if (IsGrounded(500f))
        {
            Debug.Log("inside contitional");
            rb.AddForce(new Vector2(0, 1000));
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

}