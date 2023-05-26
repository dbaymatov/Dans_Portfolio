using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteScript : MonoBehaviour
{
    
    public bool mute;
    
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        mute = false;
        
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mute)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
    }

    public void OnButton(bool value)
    {
        
        if (value == true)
        {
            mute = false;
            
        }else if(value == false)
        {
            mute = true;
            
        }
    }
}
