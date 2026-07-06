using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesScript : MonoBehaviour
{
    public float[] choiceValues = new float[8];
    //[SerializeField] Transform[] locations;
    //[SerializeField] GameObject[] choiceButtons;
    //[SerializeField] PlayScript player;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject sliderChoice;
    public float value;
    public int selection = 0;
    [SerializeField] Text valueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        value = sliderChoice.GetComponent<Slider>().value;
        if (value > 0)
        {
            choiceValues[selection] = value;
            continueButton.SetActive(true);
            
        }
        if(value == 0)
        {
            continueButton.SetActive(false);
        }
        
    }
    
    public void textUpdate(float value)
    {
        valueText.text = "" + value;
    }
}
