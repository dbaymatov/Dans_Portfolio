using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Motor:MonoBehaviour
{

    public abstract void MoveVertical(Vector2 move);
    public abstract void MoveHorizontal(Vector2 move);
}
