using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiritController : BotController
{
    [SerializeField] Collider2D DetectionTrigger;
    [SerializeField] Transform attackTrigger;
    public float hInput, vInput, moveSpeed;
    Transform target;

    enum botState { idle, agro }

    botState state = botState.idle;

    void BotMovement()
    {
        switch (state)
        {
            case botState.agro:
                hInput = Input.GetAxis("Horizontal");
                vInput = Input.GetAxis("Vertical");

                Vector2 direction = target.position - transform.position;

                motor.Move(direction); //y

                if (motor.CanDealDamage())
                {
                    StartCoroutine(motor.Attack());
                }

                //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                break;
            case botState.idle:

                break;



            default:

                break;

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
            state = botState.agro;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //state = botState.idle;
        }
    }


    void FixedUpdate()
    {
        BotMovement();
    }


}
