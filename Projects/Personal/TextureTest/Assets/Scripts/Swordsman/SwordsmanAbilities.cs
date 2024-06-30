using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanAbilities : Abilities
{
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPoint;
    [SerializeField] float damage;
    public LayerMask enemyLayers;
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
        //animator.SetTrigger("Slash");     //will activate animator when ready   
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
        foreach (Collider2D enemy in hitEnemies){
            BotDeff botDeff = enemy.GetComponent<BotDeff>();
            botDeff.TakeDamage(damage);
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
    //draws Ability1 attacl radious
    }private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
