using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// An image will be applied under the button
// Change color of button to be darker
// Adjust the alpha each frame


public class ButtonFlash : MonoBehaviour
{
    float timerValue;
    bool countingUp;
    void TickTimer(bool isCountingUp) => timerValue += isCountingUp ? Time.deltaTime : -Time.deltaTime;
    void FlipTimer(bool condition) => countingUp = condition ? !countingUp : countingUp; // Flip the timer based on timerValue
    void CheckTimer(bool condition, float newValue) => timerValue = condition ? newValue : timerValue;
    void ProcessTimer()
    {
        FlipTimer(timerValue >= 1 || timerValue <= 0);
        CheckTimer(timerValue >= 1, 0.995f);
        CheckTimer(timerValue <= 0, 0.005f);
        TickTimer(countingUp);
    }

    // Start is called before the first frame update
    void Start()
    {
        timerValue = 0.5f; // Dont start at 0!
        countingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTimer();
        var img = GetComponent<Button>().image;
        var current = img.color;
        img.color = new Color(current.r, current.g, current.b, timerValue);
    }
}