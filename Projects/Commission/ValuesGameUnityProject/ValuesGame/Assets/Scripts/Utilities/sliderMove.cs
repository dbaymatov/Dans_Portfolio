using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderMove : MonoBehaviour
{
    [SerializeField]
    Slider sliderTest;
    bool full, stopMov;
    float secValue;

    void Start()
    {
        full = false;
        sliderTest.value = 0;
        secValue = 0;
        stopMov = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMov)
        {


            if (full)
            {
                sliderTest.value -= 0.25f;
                secValue -= 0.25f;
                if (sliderTest.value == 0)
                {
                    full = false;
                }

            }
            else
            {
                sliderTest.value += 0.25f;
                secValue += 0.25f;
                if (sliderTest.value == 100)
                {
                    full = true;
                }
            }
            if (sliderTest.value != secValue)
            {
                stopMov = true;
            }
        }
    }
    public void OnceMore()
    {
        full = false;
        stopMov = false;
        sliderTest.value = 0;
        secValue = 0;
    }
}
