using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotMotor : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    [SerializeField] public Animator anim;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackPointOffest;
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] public int damage;
    [SerializeField] public float hitStrngth;
    public bool canAttack = true;
    bool moving;

    public abstract void Move(Vector2 movementVector);
    public abstract IEnumerator Attack();

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void AttackAnim()
    {
        anim.SetTrigger("Fire");
    }
    public void MoveAnim()
    {

        if (rb.velocity.magnitude > 0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        anim.SetBool("Moving", moving);

    }

    public abstract bool CanDealDamage();

}
