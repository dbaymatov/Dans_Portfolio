using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerDeff : BotDeff
{
    public UnityEngine.UI.Image playerEnergyBar;
    //public UnityEngine.UI.Image healthBar2;

    private void Start()
    {
        controller.Push(GetComponent<Controller>()); //player controll
        alive = true;
        possesd = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        controller.Peek().ExecuteMovement();//controlls movement

    }
    private void Update()
    {
        //Debug.Log("player update");
        controller.Peek().ExecuteControlls();//controlls of the abilities
        RegenEnergy();

        //if (!controller.Peek().possesing)
        //  RegenEnergy();
        if (currentEnergy < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        HealthBarManager();
    }
    public override void RegenEnergy()//for now moved energy degeneration to EnergyManager for player
    {
        //currentEnergy += energyRegen * Time.deltaTime;
        //if (currentEnergy > maxEnergy)//in case current energy exceeds the acceptable levels of energy
        //    currentEnergy = maxEnergy;
    }
    void HealthBarManager()
    {
        playerEnergyBar.fillAmount = Mathf.Clamp(currentEnergy / maxEnergy, 0, 1);
    }


}
