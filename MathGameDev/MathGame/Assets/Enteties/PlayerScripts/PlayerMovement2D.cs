using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] float attackPointOffest;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] int damage;
    [SerializeField] float hitStrngth;
    bool moving;
    bool canMove;
    bool canAttack;
    float hInput, vInput;
    Vector2 lookDirection = Vector2.down;
    enum Axis { Vertical, Horizontal, none }
    Axis lastPressedAxis = Axis.none;

    void Start()
    {
        canMove = canAttack = true;

    }

    void Update()
    {

        //checks which axis pressed the last for movement
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastPressedAxis = Axis.Vertical;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastPressedAxis = Axis.Horizontal;
        }

        if (rb.velocity.magnitude > 0)
        {
            SetAttackPoint();//updates the direction at which player attack zone is pointing
        }
        ActivateAbilityController();
        MovementAnimationController();
    }

    void FixedUpdate()
    {
        PlayerMove();

    }

    void PlayerMove()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = GetMovementDirection().normalized;

        if (rb.velocity.magnitude < maxSpeed && canMove)//max speed restriction
        {
            rb.AddForce(inputDirection * acceleration * Time.deltaTime, ForceMode2D.Force);
        }
    }

    void ActivateAbilityController()
    {
        if (Input.GetKeyDown("1") && canAttack)
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        canAttack = false;
        canMove = false;
        animator.SetTrigger("Attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.isTrigger) continue;

            HealthManager botDeff = enemy.GetComponent<HealthManager>();
            if (botDeff is not null)
            {
                botDeff.TakeDamage(damage);
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                Vector2 pushDirection = enemy.GetComponent<Transform>().position - transform.position;
                enemyRb.AddForce(pushDirection * hitStrngth, ForceMode2D.Force);
            }

        }
        yield return new WaitForSeconds(0.35f);
        canAttack = true;
        canMove = true;

    }

    //The if statments controll the movement animations, based on the input direction will play animation in according direction, in case the character is moving into will will not play animation
    void MovementAnimationController()
    {
        Vector2 inputDirection = GetMovementDirection();

        if (inputDirection.magnitude > 0.1f && rb.velocity.magnitude > 0.1f)
        {
            moving = true;
            lookDirection = inputDirection;
        }
        else
        {
            moving = false;
        }

        animator.SetFloat("X", lookDirection.x);
        animator.SetFloat("Y", lookDirection.y);
        animator.SetBool("Moving", moving);
    }

    Vector2 GetMovementDirection()
    {
        //will take user input and based on acceleration variable will apply force on the character, untill it reaches max speed at which speed becomes constant
        //the if statements restrict player to movement in one direction at a time even if horizantal and vertical keys pressed at once,
        //will prioretise the latest key press direction

        Vector2 inputDirection = Vector2.zero;

        if (lastPressedAxis == Axis.Horizontal && Mathf.Abs(hInput) > 0)
            inputDirection = new Vector2(hInput, 0);
        else if (lastPressedAxis == Axis.Vertical && Mathf.Abs(vInput) > 0)
            inputDirection = new Vector2(0, vInput);
        else if (hInput != 0)
        {
            inputDirection = new Vector2(hInput, 0);
        }
        else if (vInput != 0)
        {
            inputDirection = new Vector2(0, vInput);
        }

        return inputDirection;
    }

    private void SetAttackPoint()
    {
        Vector2 inputDirection = GetMovementDirection();
        if (inputDirection != Vector2.zero)
        {
            Vector3 moveDirection = new Vector3(inputDirection.x, inputDirection.y, 0).normalized;
            attackPoint.position = transform.position + moveDirection * attackPointOffest;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
