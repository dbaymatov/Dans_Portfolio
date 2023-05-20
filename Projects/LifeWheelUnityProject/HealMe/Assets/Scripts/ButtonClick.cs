using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{

    AudioSource buttonClick;
    void Start()
    {
        buttonClick = GetComponent<AudioSource>();
        
    }

    public void PlayClickSound(){
        buttonClick.Play();
    }
}
