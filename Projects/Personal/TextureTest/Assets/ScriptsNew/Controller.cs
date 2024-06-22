using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Controller : MonoBehaviour
{
    public Stack<Motor> motor = new Stack<Motor>();
    public Stack<Abilities> abilities = new Stack<Abilities>();

    public abstract void ExecuteMovement();
    public abstract void ExecuteControlls();

}
