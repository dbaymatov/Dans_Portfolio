using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSideScroller : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundBoxXsize;
    [SerializeField] float groundBoxYsize;
    [SerializeField] float groundBoxYpos;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask layer;
    float hInput, vInput;

    private Vector2 moveDirection;

    void Update()
    {
        //will take user input and based on acceleration variable will apply force on the character, untill it reaches max speed at which speed becomes constant
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(
                new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * acceleration, //x
                            Input.GetAxis("Vertical") * Time.deltaTime * 0)); //y
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    public void Jump()
    {
        if(IsGrounded())
            rb.AddForce(new Vector2(0, jumpForce));
    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(groundBoxXsize, groundBoxYsize), 0, -transform.up, groundBoxYpos, layer))
            return true;
        return false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundBoxYpos, new Vector2(groundBoxXsize, groundBoxYsize));
    }
}
