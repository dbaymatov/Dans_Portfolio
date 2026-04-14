using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StarMovement : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] float scalar;
    [SerializeField] float distanceRange;
    [SerializeField] bool Relative;
    [SerializeField] bool Spiral;
    [SerializeField] bool Orbit;
    [SerializeField] bool sin;


    [SerializeField] float spiralIndex;

    SpriteRenderer spriteRenderer;

    Vector3 changeInPos, prevPos, origin;

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        prevPos = origin = obj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        changeInPos = prevPos - obj.transform.position;

        if (Relative)
        {
            MoveByRelativePosition();
        }
        else if (Spiral)
        {
            MoveBySpiralPosition();
        }
        else if (Orbit)
        {
            MoveByOrbitPosition();
        }
        else if (sin)
        {
            MoveBySinPosition();
        }
        else
        {
            transform.position += changeInPos * scalar;
        }

        prevPos = obj.transform.position;

    }

    void MoveByRelativePosition()
    {

        if (Math.Abs(transform.position.x - obj.transform.position.x) <= distanceRange)
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            transform.position -= new Vector3(0, changeInPos.y * scalar);
            spriteRenderer.color = Color.red;
        }
    }
    void MoveBySpiralPosition()
    {

        transform.position += new Vector3(changeInPos.x * math.cos((obj.transform.position.x - origin.x)/spiralIndex) * scalar *(obj.transform.position.x - origin.x) ,changeInPos.x * math.sin((obj.transform.position.x - origin.x)/spiralIndex) * scalar * (obj.transform.position.x - origin.x) );

    }
    void MoveByOrbitPosition()
    {

        transform.position += new Vector3(changeInPos.x * math.cos((obj.transform.position.x - origin.x)/spiralIndex) * scalar,changeInPos.x * math.sin((obj.transform.position.x - origin.x)/spiralIndex) * scalar);

    }
    void MoveBySinPosition()
    {

        transform.position += new Vector3(changeInPos.x * math.sin((1/(obj.transform.position.x - spiralIndex))*scalar )*distanceRange,0);

    }
}
