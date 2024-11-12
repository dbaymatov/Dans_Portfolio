using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] Rigidbody2D rb;
    float hInput, vInput;

    private Vector2 moveDirection;

    void Update()
    {
        //will take user input and based on acceleration variable will apply force on the character, untill it reaches max speed at which speed becomes constant
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(
                new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * acceleration, //x
                            Input.GetAxis("Vertical") * Time.deltaTime * acceleration)); //y
        }

    }
}
