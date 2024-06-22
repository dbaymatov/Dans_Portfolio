using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CloudsMovement : MonoBehaviour
{
    [SerializeField] float verticalParalax;
    [SerializeField] float horizontalParalax;
    [SerializeField] GameObject cloudLeft;
    [SerializeField] GameObject cloudRight;

    private float length = 0;
    private Vector2 startPos;
    private Vector2 endPos;
    public GameObject cam;
    public Vector2 moveOverTime;

    void Start()
    {
        length = getLength();
        InitializeClouds();
        startPos = cam.transform.position;
        endPos = cam.transform.position;
    }

    void FixedUpdate()
    {
        endPos = startPos;
        startPos = cam.transform.position;
        Vector2 camPosDifference = endPos - startPos;

        //parallax and movement math
        transform.position = new Vector2(transform.position.x - camPosDifference.x * horizontalParalax + moveOverTime.x * Time.deltaTime,
                                         transform.position.y - camPosDifference.y * verticalParalax + moveOverTime.y * Time.deltaTime);
        //if cam get to far away from repeating obj
        if (math.abs(cam.transform.position.x - transform.position.x) > length)
        {
            Debug.Log("Geting to far");

            //move right
            if (transform.position.x < cam.transform.position.x)
            {
                Debug.Log("going right");
                transform.position = new Vector2(transform.position.x + length, transform.position.y);
                endPos = new Vector2(endPos.x + length, endPos.y);
            }
            //move left
            else if (transform.position.x > cam.transform.position.x)
            {
                Debug.Log("going left");
                transform.position = new Vector2(transform.position.x - length, transform.position.y);
                endPos = new Vector2(endPos.x - length, endPos.y);
            }
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

        cloudLeft.transform.position = new Vector2(transform.position.x-length, cloudLeft.transform.position.y);
        cloudRight.transform.position = new Vector2(transform.position.x+length, cloudLeft.transform.position.y);
        cloudRight.transform.parent = transform;
        cloudLeft.transform.parent = transform;

    }
}

