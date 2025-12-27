using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Halthbar : MonoBehaviour
{

    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        //slider.gameObject.SetActive(currentHealth < maxHealth);
        slider.gameObject.SetActive(currentHealth >= 0);

        slider.value = currentHealth;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }


    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
