using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDeff : BotDeff
{
    private void Start()
    {
        controller.Push(GetComponent<Controller>()); //initialises bot controll
        abilities = GetComponent<Abilities>();
        alive = true;
        possesd = false;
        energyRegen = durability = 200;
    }
}
