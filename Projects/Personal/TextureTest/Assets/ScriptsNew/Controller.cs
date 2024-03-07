using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Controller : MonoBehaviour
{
    public Stack<Motor> motor = new Stack<Motor>();
    public Stack<Abilities> abilities = new Stack<Abilities>();

    public abstract void TakeAction();
    public abstract void ExecuteControlls();

    public abstract void AddMotor(Motor motor);
    public abstract void RemoveMotor();
    public abstract void AddAbility(Abilities motor);
    public abstract void RemoveAbility();
}
