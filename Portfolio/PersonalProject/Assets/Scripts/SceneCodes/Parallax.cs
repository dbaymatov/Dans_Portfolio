using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float verticalParalax;
    [SerializeField] float horizontalParalax;

    private Vector2 startPos;
    private Vector2 endPos;
    public GameObject cam;

    void Start()
    {
        startPos = cam.transform.position;
        endPos = cam.transform.position;

    }

    void FixedUpdate()
    {
        Vector2 camPosDifference = endPos - startPos;

        endPos = startPos;
        startPos = cam.transform.position;
        //parallax math
        transform.position = new Vector2(transform.position.x - camPosDifference.x * horizontalParalax,
                                         transform.position.y - camPosDifference.y * verticalParalax);

    }
}
