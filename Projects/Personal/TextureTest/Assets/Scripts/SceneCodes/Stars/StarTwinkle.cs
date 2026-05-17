using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StarTwinkle : MonoBehaviour
{
    [SerializeField] Light2D mylight;
    [SerializeField] float x;
    [SerializeField] float y;
    float intensity;
    int random;
    // Start is called before the first frame update
    void Start()
    {
        mylight = GetComponent<Light2D>();
        random = UnityEngine.Random.Range(1, 4);
        intensity = mylight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        mylight.intensity = Math.Abs(y * math.cos(random + intensity + Time.fixedTime / x));
    }
}
