using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHealth : MonoBehaviour
{
    [SerializeField] int botHealth;
    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        botHealth -= damage;
        checkHealth();
    }

    void checkHealth()
    {
        if (botHealth < 1)
        {
            gameObject.SetActive(false);
        }
    }

}
