using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefill : MonoBehaviour
{
    [SerializeField]float energy;
    //public static EnergyManager eg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EnergyManager.Instance.GiveEnergy(energy);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Friendly"))
        {
            Debug.Log("Bot Detected");
            if (collision.gameObject.GetComponent<BotDeff>().possesd)
                EnergyManager.Instance.GiveEnergyBot(energy);
        }

    }
}
