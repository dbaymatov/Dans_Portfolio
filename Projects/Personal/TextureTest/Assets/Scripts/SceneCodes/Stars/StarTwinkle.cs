using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StarTwinkle : MonoBehaviour
{
    [SerializeField] Light2D light;
    [SerializeField] float x;
    [SerializeField] float y;
    float intensity;
    int random;
    // Start is called before the first frame update
    void Start()
    {
        random = UnityEngine.Random.Range(1, 4);
        intensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Math.Abs(y * math.cos(random + intensity + Time.fixedTime / x));
    }
}
