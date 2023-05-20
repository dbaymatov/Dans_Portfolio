using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpinScript : MonoBehaviour
{
    public bool[] choices = new bool[] {false, false, false, false, false, false, false, false};

    bool finished = false;
    

    public GameObject wheel;
    public WheelScript wheelS;
    [SerializeField] GameObject playManager;
    PlayScript play;

    public int pick = 0;
    public int CompletedValues => pick;
    int count = 0;

    float spin = 0f;
    float mod = 0f;
    // Start is called before the first frame update
    void Start()
    {
        wheelS = wheel.GetComponent<WheelScript>();
        play = playManager.GetComponent<PlayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        count = 0;
        for(int i = 0; i < choices.Length; i++)
        {
            if(choices[i] == true)
            {
                count += 1;
            }
        }
        if(count >= 8)
        {
            finished = true;
        }
    }

    public void OnButtonClick()
    {
        if(spin > 0 && mod > 0 && !wheelS.spin)
        {
            Debug.Log($"Time Scale : {Time.timeScale}");
            wheelS.ResetWheel(spin);
        }
        
        if (!finished && !wheelS.spin)
        {
                if (pick == 0 )//Family
                {
                    spin = 10;
                    mod = 0.9929f;
                }
                else if (pick == 2 )//recreation
                {
                    spin = 10;
                    mod = 0.9921f;
                }
                else if (pick == 5)//Health
                {
                    spin = 10;
                    mod = 0.9935f;
                }
                else if (pick == 1 )//Life Purpose
                {
                    spin = 10;
                    mod = 0.9926f;
                }
                else if (pick == 3)//Contribution
                {
                    spin = 10;
                    mod = 0.9931f;
                }
                else if (pick == 7 )//work
                {
                    spin = 10;
                    mod = 0.9933f;
                }
                else if (pick == 6 )//Social life
                {
                    spin = 10;
                    mod = 0.9924f;
                }
                else if (pick == 4 )//Finance
                {
                    spin = 10;
                    mod = 0.9937f;
                }
            play.NextClick();
            
            wheelS.SpinWheel(spin, mod);
            pick += 1;
        }
    }
}
