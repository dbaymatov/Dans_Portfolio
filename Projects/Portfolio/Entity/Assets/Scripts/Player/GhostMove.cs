using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;

    float hInput, vInput;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (rb.velocity.magnitude < 3)
        {

            rb.AddForce(
                new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, //x
                            Input.GetAxis("Vertical") * Time.deltaTime * speed)); //y
        }

        /*
        if (rb.velocity.magnitude < 3)
        {

            rb.velocity= rb.velocity * new Vector2(3 / rb.velocity.magnitude, 3 / rb.velocity.magnitude);
        }
        */
    }
}
