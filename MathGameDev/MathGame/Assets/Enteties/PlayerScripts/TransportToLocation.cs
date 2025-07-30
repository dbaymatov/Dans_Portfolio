using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransportToLocation : MonoBehaviour
{
    [SerializeField] string SendToLocation;
    
    void OnTriggerEnter2D(Collider2D other){

        Debug.Log("Touch detected");
        if(other.tag=="Player"){
            SceneManager.LoadScene(SendToLocation);
        }
    }
}
