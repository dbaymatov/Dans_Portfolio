using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{
    // bool used to track whether the wheel should be spinning or not
    public bool spin = false;

    //Used to reset the spin of the wheel to its original position
    Quaternion originalRot;

    //Bools to help determine which choice on the wheel is still available for the first time of it's use
    /*
    bool redP = false;
    bool blueP = false;
    bool greenP = false;
    bool purpP = false;
    bool indP = false;
    bool pinkP = false;
    bool orngP = false;
    bool limeP = false;
    */
    public float rotSpeed = 0;
    float rotReset = 0;
    float rotMod = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (spin)
        {
            rotSpeed = 106;
            rotReset = rotSpeed;
            
        }
        */
        /*
         * Different starting spins
         */

        /*
         *      Speed           Pairs With
         * rotSpeed = 99;       Options A & C
         * rotSpeed = 100;      Options B & C
         * rotSpeed = 101;      Option A
         * rotSpeed = 102;      Option A
         * rotSpeed = 103;      Option C
         * rotSpeed = 106;      Option A
         */
        transform.Rotate(0, 0, rotSpeed);

        rotSpeed *= rotMod;
        //rotSpeed *= Time.deltaTime;
        
        /*
         * Different slowdown speeds for each result
         */

         /* 
         * rotSpeed *= 0.96f;       Option A
         * rotSpeed *= 0.97f;       Option B
         * rotSpeed *= 0.99f;       Option C
         */
         
        
         // Resets the rotation
        
        
    }

    public void SpinWheel(float speed, float modify)
    {
        Debug.Log("Spin only!");
        Time.timeScale = 10.0f;
        rotSpeed = speed;
        rotMod = modify;
        spin = true;
    }

    public void ResetWheel(float speed)
    {
        Debug.Log("Reset only!");
        Time.timeScale = 10.0f;
        rotReset = speed;
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRot, Time.time * rotReset);
        spin = false;
    }
}
