using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SpiritMotor : BotMotor
{

    [SerializeField] float attackCooldown;
    public override void Move(Vector2 movementVector)
    {
        rb.AddForce(movementVector * Time.deltaTime * speed);
        MoveAnim();
    }
    public override IEnumerator Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                HealthManager botDeff = enemy.GetComponent<HealthManager>();
                botDeff.TakeDamage(damage);
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                Vector2 pushDirection = enemy.GetComponent<Transform>().position - transform.position;
                enemyRb.AddForce(pushDirection * hitStrngth, ForceMode2D.Force);
            }
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }

    public override bool CanDealDamage()
    {
        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        return hitEnemies != null;
    }
}
