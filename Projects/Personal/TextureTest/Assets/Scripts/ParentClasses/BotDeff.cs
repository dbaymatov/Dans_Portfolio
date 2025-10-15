using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class BotDeff : MonoBehaviour
{
    public bool alive;
    public bool possesd;
    public float maxEnergy, energyRegen, currentEnergy;
    public float durability;
    public Stack<Controller> controller = new Stack<Controller>();
    public Abilities abilities;
    public Motor motor;
    [SerializeField] GameObject aliveSprite;
    [SerializeField] GameObject deadSprite;
    [SerializeField] float aliveDurability;


    public void Destroy()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage(float damage)
    {
        durability = durability - damage;
        CheckCondition();
    }
    public void Die()
    {
        Motor motor = GetComponent<Motor>();
        alive = false;
        motor.BecomeUndead();
        //energyRegen = 0;
        GetComponent<Controller>().enabled = false; //disables the AI
    }
    public void CheckCondition()
    {
        //once durability runs out removes mob
        if (durability < 1)
        {
            Destroy();
        }
        //if durability is less then certain amount makes bot into dead state
        else if (durability < aliveDurability)
        {
            Die();
            aliveSprite.SetActive(false);
            deadSprite.SetActive(true);
        }
    }
    public virtual void RegenEnergy()//will regen mob energy if alive and current energy does not exceed max energy
    {
        if (alive && currentEnergy <= maxEnergy)
        {
            currentEnergy += energyRegen * Time.deltaTime;
        }
        if (currentEnergy > maxEnergy)//in case current energy exceeds the acceptable levels of energy
            currentEnergy = maxEnergy;
    }

    public float LoseEnergy(float absorbRate)//will be subtracting energy when possesing, if out of energy will consume durability untill dies
    {
        //note: the regen energy will be called before this function, thus absorbption will happend after regeneration 

        if (alive)
        {
            if (currentEnergy < absorbRate)//if not enough energy to satisfy absorption will go into durabilty debt
            {
                float sendEnergy = currentEnergy;
                absorbRate -= currentEnergy;//will take whatever energy there left and make debt smaller
                currentEnergy = 0;
                if (durability < absorbRate)
                {
                    sendEnergy += durability;
                    durability -= absorbRate; //durability becomes less then 0 and mob is gone
                }
                else
                {
                    sendEnergy += absorbRate;
                    durability -= absorbRate; //durability will cover up the energy debt
                }
                return sendEnergy;
            }
            else//if enough energy to pay absorption, will not eat durability and just cover it up with energy reserve
            {

                currentEnergy -= absorbRate;
                return absorbRate;
            }
        }
        Debug.Log("absorbing -tive energy"+(-energyRegen));

        return -energyRegen; //if not alive will send negative energy regen increasing energy loss to player for possesing dead mob
    }

    public void ReciveEnergy(float energy)
    {
        currentEnergy += energy;
    }

    private void FixedUpdate()
    {
        if (!possesd)//will stop controlling it self if possesd
            controller.Peek().ExecuteMovement();
    }
    private void Update()
    {

        if (!possesd)//will stop controlling it self if possesd
            controller.Peek().ExecuteControlls();

        RegenEnergy();//energy drain will be called by singleton energy manager, if player is possesing
        CheckCondition();
    }

}
