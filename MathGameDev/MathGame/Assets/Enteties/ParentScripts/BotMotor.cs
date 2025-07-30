using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotMotor : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    [SerializeField] Animator anim;
    bool moving;

    public abstract void Move(Vector2 movementVector);


    void Die()
    {
        gameObject.SetActive(false);
    }

    void Attack()
    {
        //TODO: implement atack

        AttackAnim();
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
}
