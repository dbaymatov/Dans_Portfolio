using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOpen : MonoBehaviour
{
    public GameObject Panel; 

    void Start(){
        
    }

    public void TriggerPanel(){
        if(Panel != null){
            Panel.SetActive(true);
        }
    }
}
