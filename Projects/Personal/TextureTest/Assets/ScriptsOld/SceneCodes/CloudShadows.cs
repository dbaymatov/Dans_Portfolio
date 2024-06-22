using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CloudShadows : MonoBehaviour
{
    [SerializeField] GameObject shadowsLeft;
    [SerializeField] GameObject shadowsRight;

    private float length = 0;
    private Vector2 startPos;
    private Vector2 endPos;
    public Vector2 moveOverTime;

    void Start()
    {
        length = getLength();
        InitializeClouds();
        startPos = transform.position;
        Debug.Log("Length:"+ length);

    }

    void FixedUpdate()
    {
        Vector2 camPosDifference = endPos - startPos;

        //movement math
        transform.position = new Vector2(transform.position.x - camPosDifference.x  + moveOverTime.x * Time.deltaTime,
                                         transform.position.y - camPosDifference.y  + moveOverTime.y * Time.deltaTime);
        //if it reaches length distance returns to starting position
        if (math.abs(startPos.x - transform.position.x) > length)
        {
            Debug.Log("Geting to far");

           transform.position = startPos;
        }
    }
    //gets total length of the cloud group based on the distance of the left and right most objects
    float getLength()
    {
        float left = 0;
        float right = 0;


        foreach (Transform child in transform)
        {
            if (child.transform.localPosition.x > right)
            {
                right = child.transform.localPosition.x;
            }
            if (child.transform.localPosition.x < left)
            {
                left = child.transform.localPosition.x;
            }
        }

        length = right - left;
        return length;
    }

    private void InitializeClouds()
    {

        shadowsLeft.transform.position = new Vector2(transform.position.x-length, shadowsLeft.transform.position.y);
        shadowsRight.transform.position = new Vector2(transform.position.x+length, shadowsLeft.transform.position.y);
        shadowsRight.transform.parent = transform;
        shadowsLeft.transform.parent = transform;

    }
}

