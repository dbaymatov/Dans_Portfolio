using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    public abstract void Unposses();
    public abstract void Attack();
    public abstract void Deffend();
    public abstract void Interact();
    public abstract void SpecialAbility();
    public abstract void ExecuteAbility();
    public abstract void Ability1();
    public abstract void Ability2();
    public abstract void Ability3();
    public abstract void Ability4();

    public Motor motor;

    void Start()
    {
        motor = GetComponent<Motor>();
    }

}
