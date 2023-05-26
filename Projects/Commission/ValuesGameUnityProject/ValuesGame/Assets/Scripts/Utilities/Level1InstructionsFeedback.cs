using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1InstructionsFeedback : MonoBehaviour
{
    // Instructions Elements
    [SerializeField] GameObject ValuesText, ValuesDescription;
    [SerializeField] GameObject StopWatchIcon, SecondsText;
    [SerializeField] GameObject SliderIcon, SliderFillArea, VeryImportantText, NotImportantText;
    [SerializeField] GameObject AcceptButtonText;
    [SerializeField] GameObject HomeButton;
    L1_Instruction current;

    bool showValues, showStopwatch, showSliders, showAcceptbutton;
    float MaxflashIntensity;
    float timerValue;
    [SerializeField] float ValuesTextSize, ValuesDescriptionSize, SecondsTextSize, VeryImportantTextSize, NotImportantTextSize, AcceptButtonTextSize;
    bool countingUp;
    bool instructionsEnded;

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
        MaxflashIntensity = 4; // This is the scale the timerValue for the flash intensity
        timerValue = 0.5f; // Dont start at 0!
        instructionsEnded = false;
        countingUp = true;
        current = L1_Instruction.NONE;
    }

    // Called from within the Level Instructions Classes
    public void ShowUIElement(L1_Instruction next)
    {
        DisableAll();
        switch(next)
        {
            default:
            case L1_Instruction.NONE:
            Debug.LogWarning($"Not a valid instruction. Instruction:{Enum.GetName(typeof(L1_Instruction), next)} not set to valid value. Choose \"Values\", \"Stopwatch\", \"Sliders\", \"Accept\".");
                break;
            case L1_Instruction.Values: EnableValues(true);
                break;
            case L1_Instruction.Stopwatch: EnableStopWatch(true);
                break;
            case L1_Instruction.Sliders: EnableSlider(true);
                break;
            case L1_Instruction.Accept: EnableAcceptButton(true);
                break;
            case L1_Instruction.Home: EnableHomeButton(true);
                break;

            case L1_Instruction.END: instructionsEnded = true;
                break;
        }
        current = next;
    }

    void Update()
    {
        if(!instructionsEnded)
        {
            ProcessTimer();
            // Determine which instruction we are on
            switch(current)
            {
                default:
                case L1_Instruction.NONE:
                case L1_Instruction.END:
                    DisableAll();
                 //   Debug.Log($"No instructions to process at this time");
                    break;
                case L1_Instruction.Values: FlashValues(timerValue);
                    break;
                case L1_Instruction.Stopwatch: FlashStopwatch(timerValue);
                    break;
                case L1_Instruction.Sliders: FlashSlider(timerValue);
                    break;
                case L1_Instruction.Accept: FlashAcceptButton(timerValue);
                    break;
                case L1_Instruction.Home: FlashHomeButton(timerValue);
                    break;
            }
        }
    }

    void Enable(GameObject obj, bool condition) => obj.SetActive(condition);
    void EnableValues(bool condition)
    {
        Enable(ValuesText, condition);
        Enable(ValuesDescription, condition);
    }
    void EnableStopWatch(bool condition)
    {
        Enable(StopWatchIcon, condition);
        Enable(SecondsText, condition);
    }

    void EnableSlider(bool condition)
    {
        Enable(SliderIcon, condition);
        Enable(SliderFillArea, condition);
        Enable(VeryImportantText, condition);
        Enable(NotImportantText, condition);
    }

    void EnableAcceptButton(bool condition) => Enable(AcceptButtonText, condition);
    void EnableHomeButton(bool condition) => Enable(HomeButton, condition);

    void DisableAll()
    {
        EnableValues(false);
        EnableStopWatch(false);
        EnableSlider(false);
        EnableAcceptButton(false);
        EnableHomeButton(false);
        FlashText(ValuesText, 0, ValuesTextSize);
        FlashText(ValuesDescription, 0, ValuesDescriptionSize);
        FlashText(SecondsText, 0, SecondsTextSize);
        FlashText(VeryImportantText, 0, VeryImportantTextSize);
        FlashText(NotImportantText, 0, NotImportantTextSize);
        FlashText(AcceptButtonText, 0, AcceptButtonTextSize);
        SecondsText.GetComponent<TextMeshProUGUI>().fontSharedMaterial.SetFloat(Shader.PropertyToID("_SpecularPower"), 2);
    }

    // https://gamedev.stackexchange.com/questions/201097/how-to-vary-the-glow-settings-for-a-text-mesh-pro-font

    void FlashValues(float value)
    {
        //Debug.Log($"Values Flash value: {value}");

        // Flash the text first
        FlashText(ValuesText, value, ValuesTextSize);
        FlashText(ValuesDescription, value, ValuesDescriptionSize);

        // Flash the image
            // No images here...
    }

    void FlashText(GameObject obj, float value, float fontSize)
    {
        var vText = obj.GetComponent<TextMeshProUGUI>();
        vText.fontSize = fontSize + (value * MaxflashIntensity);
        vText.fontSharedMaterial.SetFloat(Shader.PropertyToID("_GlowPower"), value);
        vText.fontSharedMaterial.SetFloat(Shader.PropertyToID("_SpecularPower"), value * MaxflashIntensity);
        vText.color = new Color(1 - (value * 0.25f), 1, 1 - (value * 0.75f), 1);
    }
    void FlashStopwatch(float value)
    {
        Debug.Log($"Stopwatch Flash value: {value}");

        // Flash the text first
        FlashText(SecondsText, value, SecondsTextSize);
        // Flash the image
    }
    void FlashSlider(float value)
    {
        Debug.Log($"Slider Flash value: {value}");

        // Flash the text first
        FlashText(VeryImportantText, value, VeryImportantTextSize);
        FlashText(NotImportantText, value, NotImportantTextSize);
        // Flash the image
    }
    void FlashAcceptButton(float value)
    {
        Debug.Log($"Accept Flash value: {value}");

        // Flash the text first
        FlashText(AcceptButtonText, value, AcceptButtonTextSize);
        // Flash the image
    }

    void FlashHomeButton(float value)
    {
        Debug.Log($"Home Flash value: {value}");

        // Flash the text first
            // no text here...
        // Flash the image
    }
}
