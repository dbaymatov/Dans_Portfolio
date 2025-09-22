using System.Collections.Generic;
using UnityEngine;
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
        energyRegen = 0;
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
    public void RegenEnergy()
    {
        if (alive && currentEnergy < maxEnergy)
        {
            currentEnergy += energyRegen * Time.deltaTime;
        }
        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy*Time.deltaTime;
    }

    public void AbsorbEnergy(float absorbRate)//will be subtracting energy when possesing, if out of energy will consume durability untill dies
    {
        if (alive)
        {
            if (currentEnergy < absorbRate)
            {
                absorbRate -= currentEnergy*Time.deltaTime;
                durability -= absorbRate*Time.deltaTime;

            }
            else
            {
                currentEnergy -= absorbRate;
            }
        }
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
        else
            AbsorbEnergy(0);

        RegenEnergy();
        CheckCondition();
    }

}
