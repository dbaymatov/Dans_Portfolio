using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotControll : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;

    float hInput, vInput;

    private Vector2 moveDirection;


    bool lookingRight;

    // Start is called before the first frame update
    void Start()
    {
        lookingRight = transform.localScale.x>0 ? true: false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookingRight = transform.localScale.x>0 ? true: false;
        
        rb.AddForce(
                new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, //x
                            Input.GetAxis("Vertical") * Time.deltaTime * 0)); //y

        if (rb.velocity.magnitude < 3)
        {
            Vector3 currentScale = transform.localScale;

            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") < 0 && !lookingRight)
                {
                    currentScale.x *= -1;
                }

                if (Input.GetAxis("Horizontal") > 0 && lookingRight)
                {
                    currentScale.x *= -1;
                }
                animator.Play("ratRun");
            }

            else
            {
                animator.Play("ratIdle");
            }

        transform.localScale = currentScale;

            
        }

        /*
        if (rb.velocity.magnitude < 3)
        {

            rb.velocity= rb.velocity * new Vector2(3 / rb.velocity.magnitude, 3 / rb.velocity.magnitude);
        }
        */
    }
}
