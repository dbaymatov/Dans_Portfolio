using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// TEST
public class Level2InstructionsFeedback : MonoBehaviour
{
    [SerializeField] GameObject stopwatch, secondstext, valuesPair, counter;
    [SerializeField] GameObject text1, text2;
    [SerializeField] float secondsTextSize, text1Size, text2Size, counterTextSize;
    float MaxflashIntensity;
    float timerValue;
    bool countingUp;
    bool instructionsEnded;
    L2_Instruction current;
    void TickTimer(bool isCountingUp) => timerValue += isCountingUp ? Time.deltaTime : -Time.deltaTime;
    void FlipTimer(bool condition) => countingUp = condition ? !countingUp : countingUp; // Flip the timer based on timerValue
    void CheckTimer(bool condition, float newValue) => timerValue = condition ? newValue : timerValue;
    void ProcessTimer()
    {
        FlipTimer(timerValue >= 1 || timerValue <= 0);
        CheckTimer(timerValue >= 1, 0.95f);
        CheckTimer(timerValue <= 0, 0.05f);
        TickTimer(countingUp);
    }
    // Start is called before the first frame update
    void Start()
    {
        MaxflashIntensity = 4.0f;
        timerValue = 0.5f; // Dont start at 0!
        instructionsEnded = false;
        countingUp = true;
        current = L2_Instruction.NONE;
    }

    public void ShowUIElement(L2_Instruction next)
    { 
        DisableAll();
        switch(next)
        {
            default:
            case L2_Instruction.NONE:
            // Debug.LogWarning($"Not a valid instruction. Instruction:{Enum.GetName(typeof(L1_Instruction), next)} not set to valid value. Choose \"Values\", \"Stopwatch\", \"Sliders\", \"Accept\".");
                break;
            case L2_Instruction.ValuePair1: EnableValuePair(true);
                break;
            case L2_Instruction.ValuePair2: EnableValuePair(true);
                break;
            case L2_Instruction.Stopwatch: EnableStopWatch(true);
                break;
            case L2_Instruction.Counter: EnableCounter(true);
                break;
            case L2_Instruction.END: instructionsEnded = true;
                break;
        }
        current = next;
        if(instructionsEnded)
        {
            Debug.Log("Instructions Ended");
            EnableCounter(true); // This MUST remain active
            FlashText(counter, 0, counterTextSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!instructionsEnded)
        {
            ProcessTimer();
            switch(current)
            {
                default:
                case L2_Instruction.NONE:
                    break;
                case L2_Instruction.END:
                    DisableAll();
                    // Debug.Log($"No instructions to process at this time");
                    break;
                case L2_Instruction.ValuePair1: FlashValues(timerValue);
                    break;
                case L2_Instruction.ValuePair2: FlashValues(timerValue);
                    break;
                case L2_Instruction.Stopwatch: FlashStopwatch(timerValue);
                    break;
                case L2_Instruction.Counter: FlashCounter(timerValue);
                    break;
            }
        }
    }

    void Enable(GameObject obj, bool condition) => obj.SetActive(condition);
    void EnableStopWatch(bool condition) => Enable(stopwatch, condition);
    void EnableValuePair(bool condition) => Enable(valuesPair, condition);
    void EnableCounter(bool condition) => Enable(counter, condition);
    void DisableAll()
    {
        EnableStopWatch(false);
        EnableValuePair(false);
        EnableCounter(false);
        FlashText(secondstext, 0, secondsTextSize, true);
        FlashText(text1, 0, text1Size);
        FlashText(text2, 0, text2Size);
        FlashText(counter, 0, counterTextSize);
    }

    void FlashText(GameObject obj, float value, float fontSize)
    {
        try
        {
            var vText = obj.GetComponent<TextMeshProUGUI>();
            vText.fontSize = fontSize + (value * MaxflashIntensity);
            vText.fontSharedMaterial.SetFloat(Shader.PropertyToID("_GlowPower"), value);
        }
        catch(Exception e)
        {
            throw new UnityException($"FlashText error - {e.Message} - ", e.InnerException);
        }
    }
    void FlashText(GameObject obj, float value, float fontSize, bool adjustColor)
    {
        FlashText(obj, value, fontSize); // error checking should have passed when method returns.
        if(adjustColor)
        {
            obj.GetComponent<TextMeshProUGUI>().color = new Color(1 - (value * 0.25f), 1, 1 - (value * 0.75f), 1);
        }
    }

    void FlashValues(float value)
    {
        FlashText(text1, value, text1Size);
        FlashText(text2, value, text2Size);
    }
    void FlashStopwatch(float value)
    {
        FlashText(secondstext, value, secondsTextSize, true); // use variant to change text color on clock
    }
    void FlashCounter(float value)
    {
        FlashText(counter, value, counterTextSize);
    }
}
