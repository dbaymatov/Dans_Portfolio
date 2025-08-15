using UnityEngine;

public abstract class Motor : MonoBehaviour
{
    public bool lookingRight = false;
    public bool moving;
    public bool falling;
    public Rigidbody2D rb;
    [SerializeField] float groundBoxXsize;
    [SerializeField] float groundBoxYsize;
    [SerializeField] float groundBoxYpos;
    [SerializeField] public Animator anim;
    public abstract void ExecuteMove(Vector2 move);
    public abstract void MoveVertical(Vector2 move);
    public abstract void MoveHorizontal(Vector2 move);
    public abstract void SpecialMove();
    public abstract void Jump();
    public abstract void BecomeUndead();
    public abstract void Animate();
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //will flip the location of the attack point for the bots that have active abilities and assymetric left/right animations such as swordsman
    public void FlipAttackPoint(Transform attackPoint)
    {
        Vector3 oldLocation = attackPoint.localPosition;
        attackPoint.localPosition = new Vector3(oldLocation.x * -1, oldLocation.y, oldLocation.z);
        lookingRight = !lookingRight;
    }
    //will flip vertically the entire game object, only for bots that have symmetric animation such as bird and rat
    public void ScaleVertical(float direction)
    {
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }
    public bool IsGrounded()//checks if the player is colliding with groind
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(groundBoxXsize, groundBoxYsize), 0, -transform.up, groundBoxYpos))
            return true;
        return false;
    }
    private void OnDrawGizmos()//draws ground checking box
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundBoxYpos, new Vector2(groundBoxXsize, groundBoxYsize));
    }

}