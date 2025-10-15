using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance { get; private set; }

    public BotDeff possesedMob;
    [SerializeField] private float absorbRate;
    public BotDeff playerDeff;

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
        PlayerEnergyDecay();
        ManagePlayerEnergy();
    }

    public void ManagePlayerEnergy()
    {
        if (possesedMob is not null)
        {
            if (playerDeff.currentEnergy < playerDeff.maxEnergy)
            {
                float neededEnergy = playerDeff.maxEnergy - playerDeff.currentEnergy;

                if (neededEnergy > absorbRate)//when player is very hungry for energy will conume it at absorption rate
                {
                    float energy = possesedMob.LoseEnergy(Time.deltaTime * absorbRate); // for now abosrption rate will be constant, can be changed later for ballancing
                    playerDeff.currentEnergy += energy; //the player can only recive energy from here
                }
                else
                {
                    playerDeff.currentEnergy += possesedMob.LoseEnergy(neededEnergy);//when player energy fills up and is just maintaining its high levels without wasting extra
                    Debug.Log("slow burn");
                }
            }

        }
        else//potential temp else, can be moved to player deff script
        {

        }
    }

    void PlayerEnergyDecay()
    {
        playerDeff.currentEnergy += playerDeff.energyRegen * Time.deltaTime;
        if (playerDeff.currentEnergy > playerDeff.maxEnergy)//in case current energy exceeds the acceptable levels of energy
            playerDeff.currentEnergy = playerDeff.maxEnergy;
    }
}