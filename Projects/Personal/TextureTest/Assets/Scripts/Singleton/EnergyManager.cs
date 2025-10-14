using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance { get; private set; }

    public BotDeff possesedMob;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
    }

    void Update()
    {
        ManageEnergy();
    }
    
    public void ManageEnergy()
    {
        if (possesedMob is not null)
        {
            Debug.Log(possesedMob);

        }
    }
}