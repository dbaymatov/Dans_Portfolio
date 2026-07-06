using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueScript : MonoBehaviour
{
    public string valueName1 = "empty";
    public string valueName2 = "empty";
    public string valueName3 = "empty";
    public string finalValue = "";
    [SerializeField] public string[] answers;
    [SerializeField] GameObject[] gremlins, gremButtons;
    [SerializeField] GameObject smartGoalsButtons;
    [SerializeField] GameObject confirmButton, textContainer;
    [SerializeField] Text textField;
    [SerializeField] InputField answer;
    [SerializeField] GameObject answerField;
    [SerializeField] GameObject saveButton;

    [SerializeField] PlayScript play;

    [SerializeField] GameObject resultScreen;

    int selectedValue, selectedValue1, selectedValue2, selectedValue3;
    int valueSelection = 4;
    int contText = 0;
    int value = 0;
    // Start is called before the first frame update

    void Start()
    {
        selectedValue1 = 0;
        selectedValue2 = 0;
        selectedValue3 = 0;

        //    selectedValue1 = -1;
        //    selectedValue2 = -1;
        //    selectedValue3 = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (answerField.activeInHierarchy)
        {
            if(answer.text != "")
            {
                confirmButton.SetActive(true);
            }
            else
            {
                confirmButton.SetActive(false);
            }
        }
        else if(contText < 20)
        {
            confirmButton.SetActive(true);
        }
        else
        {
            confirmButton.SetActive(false);
        }
        if (valueName1 == "")
        {
            textField.text = "To start planning your goal, first, select 3 bees that represents the aspects of your life that you want to work on. They will guide you through the rest of the planning process. Once you have selected your 3 choose 1 to make a goal on!";
            confirmButton.SetActive(false);
        }
        if(finalValue != "")
        {

            if (contText == 0)
            {
                textField.text = "Hey there! Let's get planning on your new goal. I invite you to think of one goal that is related to " + finalValue + ". Please write down your "+ finalValue+" goal below.";
                answerField.SetActive(true);

            }
            if(contText == 1)
            {
                textField.text = "Your current goal is: " + answers[0] + "\n\nNow, let's take a few moments to think about your " + finalValue + " goal. Ask yourself: Why are you working on this habit? How is it linked to your values and who you want to be in life?";
            }
            if (contText == 2)
            {
                textField.text = "Your current goal is: " + answers[0] + "\n\nBe as explicit and detailed as possible, don’t hold back. Write down all the thoughts and feelings that came to you in the text box below.";
                answerField.SetActive(true);
            }
            if (contText == 3)
            {
                textField.text = "Your current goal is: " + answers[0] + "\n\nGreat work! You have chosen a healthy habit that is linked to your values! We are now going to make your goal a SMART goal!";
            }
            if (contText == 4)
            {
                textField.text = "Your current goal is: " + answers[0] + "\n\nA SMART goal is Specific, Measurable, Achievable, Realistic, and Time-bound. In the next slides, we will answer a series of questions to help make your goal a SMART goal!";
            }
            if (contText == 5)
            {
                textField.text = "Your current goal is: " + answers[0] + "\n\nStep 1: Is my habit a behavioural goal?\tFirst, lets make sure your habit is a behavioural goal! Make it tangible, observable and explicit. For example \"I want to enjoy exercise\" could become \"I will complete 30 minutes of exercise 3 times a week.\"";
                
            }
            if (contText == 6)
            {
                textField.text = "Your current goal is: " + answers[0] + "\n\nPlease edit it to make it a behavioural goal. If you want to skip this step please copy your previous goal into the text below.";
                answerField.SetActive(true);
            }
            if (contText == 7)
            {
                textField.text = "Your current goal is: " + answers[2] + "\n\nWay to go! You have made your goal specific and measurable.";
                
            }
            if (contText == 8)
            {
                textField.text = "Your current goal is: " + answers[2] + "\n\nStep 2: Is my habit a \"do instead\" goal or a \"don\'t do\" goal?";
                
            }
            if (contText == 9)
            {
                textField.text = "Your current goal is: " + answers[2] + "\n\nAn example of a \"don\'t do\" goal is - I will stop drinking pop. An example of a \"do instead\" goal is - I will drink zero calorie flavoured water instead of pop.";
                
            }
            if (contText == 10)
            {
                textField.text = "Your current goal is: " + answers[2] + "\n\nWe invite you to edit your goal to make it a \"do instead\" goal!\n[If your goal already fits, please re-type it into the text box below!]";
                answerField.SetActive(true);
            }
            if (contText == 11)
            {
                textField.text = "Your current goal is: " + answers[3] + "\n\nAmazing work! You have made your goal specific and measurable.";
                
            }
            if (contText == 12)
            {
                textField.text = "Your current goal is: " + answers[3] + "\n\nStep 3: Is my habit a 90% goal? Are you 90% sure you can do this behavior in the time frame you set? If not, how can you make it smaller, less frequent or more manageable so that you're 90% sure you can accomplish it?";
                
            }
            if (contText == 13)
            {
                textField.text = "Your current goal is: " + answers[3] + "\n\nWe invite you to edit your goal to make it a 90% goal! For example \"I will go for a walk every day\" could be changed to \"I\'ll go for a 10 minute walk 3 times a week\"";
                
            }
            if (contText == 14)
            {
                textField.text = "Your current goal is: " + answers[3] + "\n\n[If you want to skip this step, please copy your goal above into the text box below!]";
                answerField.SetActive(true);
            }
            if (contText == 15)
            {
                textField.text = "Your current goal is: " + answers[4] + "\n\nWoo hoo! You have made your goal achievable and realistic!";
                
            }
            if (contText == 16)
            {
                textField.text = "Your current goal is: " + answers[4] + "\n\nStep 4: Setting a time-bound goal?\tDoes your goal have a time frame? If not, what is your time frame for accomplishing this goal? For example \"I\'ll eat more vegetables\" could become \"I\'ll have 1 serving of vegetable 3 times over the next week\"";
                
            }
            if (contText == 17)
            {
                textField.text = "Your current goal is: " + answers[4] + "\n\n[If you want to skip this step, please copy your goal above into the text box below!]";
                answerField.SetActive(true);
            }
            if (contText == 18)
            {
                textField.text = "Your current goal is: " + answers[5] + "\n\nAwesome work! You have made your goal time bound!";
            }
            if (contText == 19)
            {
                textField.text = "Your current goal is: " + answers[5] + "\n\nWay to go! you have come up with a healthy habit to work on!";
            }
            if (contText >= 20)
            {
                textField.text = "My healthy habit for " + finalValue + " is: " + answers[5] + "\n\nThe best way to achieve big goals is to take small steps. Please click the button below to save your goal. Your goal will be displayed on you dashboard.";
                saveButton.SetActive(true);
            }

        }
    }


    public void ContinueButton()
    {
        if (answer.text != "")
        {
            answers[value] = answer.text;
            value += 1;
        }
        answer.text = "";
        resultScreen.SetActive(false);
        answerField.SetActive(false);
        contText += 1;
    }


    // Pick the specific value the user wants
    public void ContinueClick()
    {
        if (valueSelection >= 3)
        {
            
            for (int i = 0; i < gremlins.Length; i++)
                {
                    if (i != selectedValue1 && i != selectedValue2 && i != selectedValue3)
                    {
                        gremlins[i].SetActive(false);
                        gremButtons[i].SetActive(false);
                }
                }
            if (valueSelection >= 4)
            {
                for (int i = 0; i < gremlins.Length; i++)
                {
                    if (i != selectedValue)
                    {
                        gremlins[i].SetActive(false);
                        gremButtons[i].SetActive(false);
                    }
                }
                Debug.Log("I worked");
                smartGoalsButtons.SetActive(false);
                confirmButton.SetActive(true);
                answerField.SetActive(true);
                textContainer.SetActive(true);
            }
        }

    }

    public void Click1()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 0;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 0;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 0;
        }
        else
        {
            selectedValue = 0;
        }


        if (valueName1 == "")
        {
            valueName1 = "family";
        }
        else if (valueName2 == "")
        {
            valueName2 = "family";
        }
        else if (valueName3 == "")
        {
            valueName3 = "family";
        }
        else
        {
            finalValue = "family";
        }
        finalValue = "family";

        valueSelection += 1;
        answerField.SetActive(false);
    }

    public void Click2()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 1;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 1;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 1;
        }
        else
        {
            selectedValue = 1;
        }


        if (valueName1 == "")
        {
            valueName1 = "life purpose";
        }
        else if (valueName2 == "")
        {
            valueName2 = "life purpose";
        }
        else if (valueName3 == "")
        {
            valueName3 = "life purpose";
        }
        else
        {
            finalValue = "life purpose";
        }
        finalValue = "life purpose";

        valueSelection += 1;
        answerField.SetActive(false);
    }

    public void Click3()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 2;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 2;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 2;
        }
        else
        {
            selectedValue = 2;
        }


        if (valueName1 == "")
        {
            valueName1 = "recreation";
        }
        else if (valueName2 == "")
        {
            valueName2 = "recreation";
        }
        else if (valueName3 == "")
        {
            valueName3 = "recreation";
        }
        else
        {
            finalValue = "recreation";
        }
        finalValue = "recreation";
        valueSelection += 1;
        answerField.SetActive(false);
    }

    public void Click4()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 3;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 3;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 3;
        }
        else
        {
            selectedValue = 3;
        }


        if (valueName1 == "")
        {
            valueName1 = "contribution";
        }
        else if (valueName2 == "")
        {
            valueName2 = "contribution";
        }
        else if (valueName3 == "")
        {
            valueName3 = "contribution";
        }
        else
        {
            finalValue = "contribution";
        }
        finalValue = "contribution";
        valueSelection += 1;
        answerField.SetActive(false);
    }

    public void Click5()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 4;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 4;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 4;
        }
        else
        {
            selectedValue = 4;
        }


        if (valueName1 == "")
        {
            valueName1 = "finance";
        }
        else if (valueName2 == "")
        {
            valueName2 = "finance";
        }
        else if (valueName3 == "")
        {
            valueName3 = "finance";
        }
        else
        {
            finalValue = "finance";
        }
        finalValue = "finance";
        answerField.SetActive(false);
    }

    public void Click6()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 5;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 5;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 5;
        }
        else
        {
            selectedValue = 5;
        }



        if (valueName1 == "")
        {
            valueName1 = "health";
        }
        else if (valueName2 == "")
        {
            valueName2 = "health";
        }
        else if (valueName3 == "")
        {
            valueName3 = "health";
        }
        else
        {
            finalValue = "health";
        }
        finalValue = "health";
        valueSelection += 1;
        answerField.SetActive(false);
    }

    public void Click7()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 6;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 6;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 6;
        }
        else
        {
            selectedValue = 6;
        }


        if (valueName1 == "")
        {
            valueName1 = "social life";
        }
        else if (valueName2 == "")
        {
            valueName2 = "social life";
        }
        else if (valueName3 == "")
        {
            valueName3 = "social life";
        }
        else
        {
            finalValue = "social life";
        }
        finalValue = "social life";
        valueSelection += 1;
        answerField.SetActive(false);
    }

    public void Click8()
    {
        if (selectedValue1 == -1)
        {
            selectedValue1 = 7;
        }
        else if (selectedValue2 == -1)
        {
            selectedValue2 = 7;
        }
        else if (selectedValue3 == -1)
        {
            selectedValue3 = 7;
        }
        else
        {
            selectedValue = 7;
        }


        if (valueName1 == "")
        {
            valueName1 = "work";
        }
        else if (valueName2 == "")
        {
            valueName2 = "work";
        }
        else if (valueName3 == "")
        {
            valueName3 = "work";
        }
        else
        {
            finalValue = "work";
        }
        finalValue = "work";
        valueSelection += 1;
        answerField.SetActive(false);
    }
}
