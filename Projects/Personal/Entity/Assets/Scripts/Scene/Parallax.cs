using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length;
    private Vector2 startPos;
    
    public GameObject cam;
    public float parallaxEffect;
    public Vector2 moveOverTime;


    float timeStamp;


    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        timeStamp = Time.time;

    }

    void FixedUpdate()
    {
        float layerLoc = (moveOverTime.x * Time.time+cam.transform.position.x) * (1 - parallaxEffect);
        Vector2 dist = new Vector2(cam.transform.position.x * parallaxEffect, cam.transform.position.y * parallaxEffect/2) + moveOverTime*Time.time;
        transform.position = new Vector2(startPos.x + dist.x, startPos.y + dist.y);


        if (layerLoc > (startPos.x + length + moveOverTime.x * Time.time + cam.transform.position.x))
        {
            float time = Time.time - timeStamp;

            startPos.x += length - moveOverTime.x * time;

            timeStamp = Time.time;
        }
        else if (layerLoc < startPos.x - length + moveOverTime.x * Time.time + cam.transform.position.x)
        {
            float time = Time.time - timeStamp;

            startPos.x -= length - moveOverTime.x * time;

            timeStamp = Time.time;
        }
    }
}
