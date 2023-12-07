using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    Motor motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<Motor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime, //x
                            Input.GetAxis("Vertical") * Time.deltaTime);

        motor.MoveHorizontal(move);
        motor.MoveVertical(move);

    }

}
