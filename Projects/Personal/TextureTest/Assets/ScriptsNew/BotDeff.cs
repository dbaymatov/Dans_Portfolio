using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BotDeff : MonoBehaviour
{
    public bool alive;
    public bool possesd;
    public float hp;
    public float energyRegen;
    public float durability;
    public Stack<Controller> controller = new Stack<Controller>();
    public Abilities abilities;
    public void Die()
    {
        alive = false;
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void CheckCondition()
    {
        if (hp < 0)
        {
            Die();
        }
        if (durability < 0)
        {
            Destroy();
        }
    }

    public abstract void Posses(Controller playerController);

}
