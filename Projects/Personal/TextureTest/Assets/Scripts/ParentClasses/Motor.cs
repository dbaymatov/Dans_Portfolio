using UnityEngine;

public abstract class Motor : MonoBehaviour
{
    public bool lookingRight = false;
    public bool moving;
    public bool falling;
    public abstract void ExecuteMove(Vector2 move);
    public abstract void MoveVertical(Vector2 move);
    public abstract void MoveHorizontal(Vector2 move);
    public abstract void SpecialMove();
    public abstract void Jump();
    public abstract void BecomeUndead();
    public abstract void Animate();
    //will change direction based on the player horizontal movement, will accept -1 +1 arguments representing left and right directions
    public void FlipAttackPoint(Transform attackPoint)
    {
        Vector3 oldLocation = attackPoint.localPosition;
        attackPoint.localPosition = new Vector3(oldLocation.x * -1, oldLocation.y, oldLocation.z);
        lookingRight = !lookingRight;
    }
    //will change direction based on the player horizontal movement, will accept -1 +1 arguments representing left and right directions
    public void ScaleVertical(float direction){

        transform.localScale = new Vector2(direction,transform.localScale.y);
    }



}