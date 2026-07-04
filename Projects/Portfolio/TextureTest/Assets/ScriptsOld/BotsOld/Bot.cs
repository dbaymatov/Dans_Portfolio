using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{

    [SerializeField] PlayerBotControll pbc;

    [SerializeField] MeshRenderer mr;
    [SerializeField] Collider2D coll;


    [SerializeField] Material defaultMaterial;
    [SerializeField] Material SelectMaterial;



    public enum botType
    {
        Undead,
        Machine,
        Alive
    }
    public botType curentType;

    public enum botState
    {
        Possesed,
        Normal,
        Dead

    }

    public float energyProduction;
    [SerializeField] private botType currentType;
    public botState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = botState.Normal;
        
    }

    // Update is called once per frame
    void Update()
    {


        switch (currentState)
        {
            case botState.Possesed:
                pbc.enabled = true;

                break;
            case botState.Normal:
                pbc.enabled = false;

                break;
            case botState.Dead:
                Die();
                break;
            default:
                break;
        }

    }


    void CheckIfDead()
    {


    }

    void CheckIfUndead()
    {


    }


    void SetUndead()
    {

        energyProduction = energyProduction * -0.1f;

    }

    public void Die()
    {
        currentState = botState.Dead;
        coll.enabled = false;
        mr.enabled = false;
        pbc.enabled = false;


    }








    public void HighLight()
    {

        mr.material = SelectMaterial;


    }

    public void StopHighlight()
    {

        mr.material = defaultMaterial;

    }

}
