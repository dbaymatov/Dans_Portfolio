using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiritController : BotController
{
    float hInput, vInput;

    enum botState { idle, agro }

    botState state = botState.idle;

    void BotAction()
    {
        switch (state)
        {
            case botState.agro:
            


            case botState.idle:

            

            default: break;

        }


    }

    void FixedUpdate()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        motor.Move(new Vector2(hInput * Time.deltaTime, //x
                            vInput * Time.deltaTime)); //y

    }


    
}
