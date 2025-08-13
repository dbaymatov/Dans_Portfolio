using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    /// <summary>
    /// Script used to controll player ghost character, it features player movement in ghost state and methods used to posses and unposses mobs.
    /// It uses collider triggers to detect which mobs are in range to be possesed and adds them to the list, the mob which is closest to the ghost is the top candidate
    /// </summary>
    List<GameObject> targetList = new List<GameObject>();//list of mobs in posses range
    GameObject target; //closest mob in the list to posses
    LayerMask initialLayer;//temp variable used to set the bot back to original layer once done possesing
    bool possesing=false;
    //collects inputs from player and executes them
    public override void ExecuteMovement()//this method goes into fixed update in deff class that will execute it
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime, //x
                                   Input.GetAxis("Vertical") * Time.deltaTime); //y
        motor.Peek().ExecuteMove(move);
        abilities.Peek().ExecuteAbility();
    }
    public override void ExecuteControlls()//this method goes into update in deff class that will execute it
    {
        if (Input.GetKeyDown("1"))
        {
            abilities.Peek().Ability1();
        }

        if (Input.GetKeyDown("2"))
        {
            abilities.Peek().Ability2();
        }
        if (Input.GetKeyDown("3"))
        {
            abilities.Peek().Ability3();
        }
        if (Input.GetKeyDown("4"))
        {
            abilities.Peek().Ability4();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            motor.Peek().Jump();
        }
        if (Input.GetKeyDown("x"))//collects user input and depending on the possesion state either posses or unposses
        {
            if (!possesing & targetList.Count > 0)
            {
                target = GetClosest();//gets closest mob in possesion range
                Possess(target);//and posses it
            }
            else//if allready possesing will unposses instead
            {
                Unposses();
            }
        }
        if (possesing)
        {
            transform.position = target.transform.position;// overrides the rb and forces the ghost position to be on top of mob position since simply paranting does not work
        }
    }

    public void AddMotor(Motor motor)//posses movement of mob
    {
        this.motor.Push(motor);
    }
    public void RemoveMotor()//give up movement of mob
    {
        if (this.motor.Count > 1)
        {
            this.motor.Pop();
        }
    }
    public void AddAbility(Abilities ability)//posses movement of mob
    {
        this.abilities.Push(ability);
    }
    public void RemoveAbility()//give up abilities of mob
    {
        if (this.abilities.Count > 1)
        {
            this.abilities.Pop();
        }
    }
    private GameObject GetClosest()
    {
        GameObject obj = targetList[0];
        float distance = Vector2.Distance(transform.position, obj.transform.position);
        foreach (GameObject item in targetList)
        {
            if (Vector2.Distance(transform.position, item.transform.position) < distance)
            {
                distance = Vector2.Distance(transform.position, item.transform.position);
                obj = item;
            }
        }
        return obj;
    }
    private void Possess(GameObject bot)
    {
        //hijacks abilities and motor of bot
        AddMotor(bot.GetComponent<Motor>());
        AddAbility(bot.GetComponent<Abilities>());
        //sets same position and parants to bot
        transform.position = bot.transform.position;
        BotDeff def = bot.GetComponent<BotDeff>();//changes layer to fiendly so to not get attacked by friendly mobs and itself
        def.possesd = true;
        possesing = true;
        initialLayer = bot.layer;
        bot.layer = LayerMask.NameToLayer("Friendly");
    }
    private void Unposses()//clears bots motor and abilites from the player control something othervise does nothing
    {
        if (possesing)
        {
            target.GetComponent<BotDeff>().possesd = false;
            target.layer = initialLayer;//changes layer back to bot
            RemoveMotor();
            RemoveAbility();
            possesing = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)//if its not possesing anything targets the bot it collides with
    {
        targetList.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)//if it not possesing anything removes the target it collided with
    {
        targetList.Remove(collision.gameObject);
    }
}
