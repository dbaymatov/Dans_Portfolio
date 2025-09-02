using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            Debug.Log("Player Dies");
        }
        else
        {
            StartCoroutine(Invulnurability());
        }

    }
}
