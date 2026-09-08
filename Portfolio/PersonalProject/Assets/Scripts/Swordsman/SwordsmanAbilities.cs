using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwordsmanAbilities : Abilities
{
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPoint;
    [SerializeField] float damage;
    [SerializeField] Animator anim;
    bool ability1 = true;
    public override void Attack()
    {
    }

    public override void Deffend()
    {
    }

    public override void ExecuteAbility()
    {
    }

    public override void Interact()
    {
    }

    public override void SpecialAbility()
    {
    }

    public override void Unposses()//will kick out player
    {
    }

    public override void Ability1()//will use as attack method for swordsman
    {
        StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {

        if (motor.IsGrounded() && ability1)
        {
            ability1 = false;
            motor.canMove = false;
            motor.FullStop();
            anim.SetTrigger("Slash");//will activate animator 
            yield return new WaitForSecondsRealtime(0.5f);
            motor.rb.velocity = Vector2.zero;//will kill any preveous movement to stop bot
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                if(enemy.gameObject == gameObject)//prevents self destruction
                {
                    continue;
                }

                BotDeff botDeff = enemy.GetComponent<BotDeff>();
                botDeff.TakeDamage(damage);
                Debug.Log(enemy.gameObject.name);
            }
            motor.canMove = true;
            ability1 = true;
            yield return new WaitForSecondsRealtime(0f);

        }
    }


    public override void Ability2()
    {
    }

    public override void Ability3()
    {
    }

    public override void Ability4()
    {
        //draws Ability1 attack radious
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
