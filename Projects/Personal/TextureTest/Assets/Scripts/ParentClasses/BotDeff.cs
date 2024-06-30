using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BotDeff : MonoBehaviour
{
    public bool alive;
    public bool possesd;
    public float energyRegen;
    public float durability;
    public Stack<Controller> controller = new Stack<Controller>();
    public Abilities abilities;
    [SerializeField] GameObject aliveSprite;
    [SerializeField] GameObject deadSprite;
    [SerializeField] float aliveDurability;

  
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage(float damage){
        durability = durability - damage;
        CheckCondition();
    }
    public void Die(){
        Motor motor = GetComponent<Motor>();
        alive = false;
        motor.BecomeUndead();
    }
    public void CheckCondition()
    {
        //if durability is less then certain amount makes bot into dead state
        if (durability < aliveDurability)
        {
            Die();
            aliveSprite.SetActive(false);
            deadSprite.SetActive(true);
        }
        //once durability runs out removes mob
        if (durability < 1)
        {
            Destroy();
        }
    }
}
