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


    //bool lookingRight;

    // Start is called before the first frame update
    void Start()
    {
        //lookingRight = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (rb.velocity.magnitude < 3)
        {
            Vector3 currentScale = transform.localScale;

            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                 //   lookingRight = false;
                    animator.Play("ratRun");
                }

                if (Input.GetAxis("Horizontal") > 0)
                {

                 //   lookingRight = true;
                    animator.Play("ratRun");

                }
            }

            if (Input.GetAxis("Horizontal") == 0)
            {
                animator.Play("ratIdle");

            }


           



            transform.localScale = currentScale;

            rb.AddForce(
                new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, //x
                            Input.GetAxis("Vertical") * Time.deltaTime * 0)); //y
        }

        /*
        if (rb.velocity.magnitude < 3)
        {

            rb.velocity= rb.velocity * new Vector2(3 / rb.velocity.magnitude, 3 / rb.velocity.magnitude);
        }
        */
    }
}
