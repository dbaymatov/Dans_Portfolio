using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LavaRise : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float InitialOffset;
    [SerializeField] Scene2DataManager dm;
    [SerializeField] MoevementManagerScene2 mv;
    [SerializeField] GameObject LavaReset;
    [SerializeField] GameObject Player;
    void Update()
    {
        //lava will rise based on the distance between it and the player object.
        //the closer the lava is to the player the slower it will rise.
        if (dm.gameOn || (mv.moving && !dm.gameEnd))
        {
            float distance =  Math.Abs(Player.transform.position.y -transform.position.y - InitialOffset);
            transform.position = new Vector3(transform.position.x,transform.position.y + distance*speed*Time.deltaTime, transform.position.z);
        }

    }

    //this method is called from the scene 2 animation manager once the player exits the scene view to reset location of the lava for the next question play
    public void ResetLava()
    {
        transform.position = LavaReset.transform.position;
    }
}
