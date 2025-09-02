using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthManager : MonoBehaviour
{
    public int health;
    public bool invincible = false;
    public SpriteRenderer sr;
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update
    public virtual void TakeDamage(int damage)
    {
        if (!invincible)
        {
            health -= damage;
            checkHealth();
        }

    }

    public IEnumerator Invulnurability()
    {
        invincible = true;
        sr.enabled = false;
        for (int i = 0; i < 3; i++) // blink 3 times
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }


        invincible = false;
    }

    void checkHealth()
    {
        if (health < 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(Invulnurability());
        }
    }

}
