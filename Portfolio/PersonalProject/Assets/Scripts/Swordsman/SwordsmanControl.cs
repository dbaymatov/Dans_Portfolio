using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanControl : Controller
{
    [SerializeField] float detectionDistance;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float attackRange;
    [SerializeField] float deaggroDistance;
    GameObject target;
    public enum State { patrol, fighting, idle, dead }
    [SerializeField] State currentState;
    public override void ExecuteMovement()//here goes movement
    {
        switch (currentState)
        {

            case State.idle:
                FindTarget(75f, 6, 360, 12, detectionDistance / 3);
                motor.Peek().MoveHorizontal(Vector2.zero);
                 if (target is not null)
                {
                    currentState = State.fighting;
                }
                
                break;
            case State.patrol:
                motor.Peek().MoveHorizontal(Vector2.zero);
                FindTarget(75f, 6, 360, 12, detectionDistance / 3);


                if (target is not null)
                {
                    currentState = State.fighting;
                }
                break;

            case State.fighting:
                if (Vector2.Distance(transform.position, target.transform.position) < attackRange && target.activeSelf)
                {
                    motor.Peek().MoveHorizontal(GetDirection());//makes bot rotate towards the target
                    abilities.Peek().Ability1();//will swing sword if close enough
                }
                else
                {
                    motor.Peek().MoveHorizontal(GetDirection());//move towards the target
                }
                if (target.activeSelf is false)
                {
                    target = null;
                    currentState = State.idle;
                }
                else if (Vector2.Distance(target.transform.position, transform.position) > deaggroDistance)//target runs away
                {
                    target = null;
                    currentState = State.idle;
                }
                break;
            default:
                break;
        }

    }
    public override void ExecuteControlls()//here go attack/abilities
    {
        motor.Peek().Animate();
    }


    void FindTarget(float angle, int rayCount, float innerAngle, int innerRayCount, float innerDistance)
    {
        //line of sight raycast
        float angleStep = angle / rayCount;
        float startAngle = -angle / 2;

        if (!motor.Peek().lookingRight)
        {
            startAngle += 180;
        }

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionDistance, targetLayer);

            if (hit.collider != null)
            {
                target = hit.collider.gameObject;
            }
            Debug.DrawRay(transform.position, direction * detectionDistance, Color.yellow);
        }

        //smaller raycast 
        float innerAngleStep = innerAngle / innerRayCount;
        float innerStartAngle = -innerAngle / 2;

        if (!motor.Peek().lookingRight)
        {
            innerStartAngle += 180;
        }

        for (int j = 0; j < innerRayCount; j++)
        {
            float currentAngle = innerStartAngle + (innerAngleStep * j);
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, innerDistance, targetLayer);

            if (hit.collider != null)
            {
                target = hit.collider.gameObject;
            }
            Debug.DrawRay(transform.position, direction * innerDistance, Color.red);
        }
    }






    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == targetLayer)
            target = collision.gameObject;
    }
    Vector2 GetDirection()
    {
        return target.transform.position - transform.position;
    }


}
