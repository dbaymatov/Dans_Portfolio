using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    List<GameObject> targetList = new List<GameObject>();
    GameObject target;
    bool possesing;
    void Start()
    {
        possesing = false;
        motor.Push(GetComponent<Motor>());
        abilities.Push(GetComponent<Abilities>());
    }

    //collects inputs from player and executes them
    public override void TakeAction()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime, //x
                                   Input.GetAxis("Vertical") * Time.deltaTime); //y
        motor.Peek().ExecuteMove(move);
        abilities.Peek().ExecuteAbility();
    }
    public override void ExecuteControlls()
    {
        if (Input.GetKeyDown("1"))
        {
            abilities.Peek().Ability1();
        }

        if (Input.GetKeyDown("2"))
        {

        }
        if (Input.GetKeyDown("3"))
        {

        }
        if (Input.GetKeyDown("4"))
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("trying to jump");
            motor.Peek().Jump();
        }
        if (Input.GetKeyDown("x"))//collects user input and depending on the possion state either posses or unposses
        {
            if (!possesing & targetList.Count > 0)
            {
                target = GetClosest();
                Possess(target);
            }
            else
            {
                Unposses();
            }
        }
        if (possesing)
        {
            transform.position = target.transform.position;// overrides the rb
        }
    }

    public override void AddMotor(Motor motor)//posses movement of mob
    {
        this.motor.Push(motor);
    }
    public override void RemoveMotor()//give up movement of mob
    {
        if (this.motor.Count > 1)
        {
            this.motor.Pop();
        }
    }
    public override void AddAbility(Abilities ability)//posses movement of mob
    {
        this.abilities.Push(ability);
    }
    public override void RemoveAbility()//give up movement of mob
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
        foreach(GameObject item in targetList) {
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
            BotDeff def = bot.GetComponent<BotDeff>();
            def.possesd = true;
            possesing = true;
    }
    private void Unposses()//clears bots motor and abilites from the player control something othervise does nothing
    {
        if (possesing)
        {
            target.GetComponent<BotDeff>().possesd = false;
            RemoveMotor();
            RemoveAbility();
            possesing = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)//if its not possesing anything targets the bot it collides with
    {
        targetList.Add(other.gameObject);
        Debug.Log(targetList);
    }
    private void OnTriggerExit2D(Collider2D collision)//if it not possesing anything removes the target it collided with
    {
        targetList.Remove(collision.gameObject);
        Debug.Log(targetList);
    }
}
