using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//data class containing neccassary data about location
[System.Serializable]
public class MovementData
{
    public Vector3 rotations;
    public bool questionActivate;
    public string animationPlay;
    public int speed;
    public GameObject targetLocationn;
    public bool changeSceneBackground;

    public enum movementType
    {

        teleport,
        translate
    }
    public movementType selectedMovement;

}
