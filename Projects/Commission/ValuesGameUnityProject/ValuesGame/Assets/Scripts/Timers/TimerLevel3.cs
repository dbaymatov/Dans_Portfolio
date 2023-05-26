using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class TimerLevel3 : MonoBehaviour
{
    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float startTime = 30.0f;
    [SerializeField] Scene3DataManager dm;


    private float currentTime;
    private bool updateTime;
    private void Start()
    {


        currentTime = startTime;
        countdownCircleTimer.fillAmount = 1.0f;

        //easy way to represent only seconds and skip the float
        countdownText.text = (int)currentTime + "s";

        //update the countdown on the update
        updateTime = true;
    }

    private void Update()
    {
        if (dm.countTime)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0.0f)
            {
                ResetTimer();
            }

            //easy way to represent only seconds and skip the float
            countdownText.text = (int)currentTime + "s";

            //clamps the value by setting the range 0.0f to 1.0f 
            float normalizedValue = Mathf.Clamp(currentTime / startTime, 0.0f, 1.0f);

            //fills the circle based on returned time
            countdownCircleTimer.fillAmount = normalizedValue;
        }
    }

    public void ResetTimer()
    {
        currentTime = startTime;
        countdownCircleTimer.fillAmount = 1.0f;
        countdownText.text = (int)currentTime + "s";
        updateTime = true;
    }

}
