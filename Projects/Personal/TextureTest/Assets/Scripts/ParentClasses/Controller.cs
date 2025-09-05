using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Stack<Motor> motor = new Stack<Motor>();
    public Stack<Abilities> abilities = new Stack<Abilities>();
    public Stack<BotDeff> botDeffs = new Stack<BotDeff>();
    public bool possesing;
    public abstract void ExecuteMovement();
    public abstract void ExecuteControlls();

    void Start()
    {
        possesing = false;
        motor.Push(GetComponent<Motor>());
        abilities.Push(GetComponent<Abilities>());
        botDeffs.Push(GetComponent<BotDeff>());

    }
}
