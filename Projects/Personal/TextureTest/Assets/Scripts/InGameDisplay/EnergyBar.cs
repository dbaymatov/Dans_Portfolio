using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{

    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;

    public void SetEnergy(float currentEnergy, float maxEnergy)
    {
        //slider.gameObject.SetActive(currentEnergy==0);
        slider.gameObject.SetActive(currentEnergy >= 0);

        slider.value = currentEnergy;
        slider.maxValue = maxEnergy;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }


    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
