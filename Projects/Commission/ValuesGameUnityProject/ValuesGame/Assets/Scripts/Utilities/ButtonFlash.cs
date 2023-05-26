using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlash : MonoBehaviour
{
    float timerValue;
    bool countingUp;
    void TickTimer(bool isCountingUp) => timerValue += isCountingUp ? Time.deltaTime : -Time.deltaTime;
    void FlipTimer(bool condition) => countingUp = condition ? !countingUp : countingUp; // Flip the timer based on timerValue
    void CheckTimer(bool condition, float newValue) => timerValue = condition ? newValue : timerValue;
    void ProcessTimer()
    {
        FlipTimer(timerValue >= 1.5 || timerValue <= 0);
        CheckTimer(timerValue >= 2, 1.45f);
        CheckTimer(timerValue <= 0, 0.05f);
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
        var col = GetComponent<Button>().image.color;
        GetComponent<Button>().image.color = new Color(col.r, col.g, col.b, 1 - (timerValue * 0.22f));
       // Debug.Log($"{col}");
    }
}
