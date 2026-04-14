using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefill : MonoBehaviour
{
    [SerializeField] float energy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("coll detected");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Friendly"))
        {
            if (collision.gameObject.GetComponent<BotDeff>().possesd)
            {
                EnergyManager.Instance.GiveEnergyBot(energy);
                collision.gameObject.GetComponent<BotDeff>().currentEnergy = collision.gameObject.GetComponent<BotDeff>().maxEnergy;// refils bot energy also to max
                gameObject.SetActive(false);
                Debug.Log("give energy to bot");

            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EnergyManager.Instance.GiveEnergy(energy);
            Debug.Log("give energy to player");
        }
    }
}
