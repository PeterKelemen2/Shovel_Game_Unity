using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI costText;

    public TextMeshProUGUI dmgText;

    //public RawImage on;
    //public RawImage off;
    public LevelGenerator2 lg;

    private int blueDMG = 1;
    private int redDMG = 2;
    private int greenDMG = 3;
    private int yellowDMG = 4;

    private int blueCost = 10;
    private int redCost = 20;
    private int greenCost = 30;
    private int yellowCost = 40;

    public int shovelCost = 0;
    public int shovelDMG = 0;

    public bool blueOwned = false;
    public bool redOwned = false;
    public bool greenOwned = false;
    public bool yellowOwned = false;

    private TextMeshProUGUI[] texts;

    void Start()
    {
        lg = FindObjectOfType<LevelGenerator2>();

        switch (gameObject.tag)
        {
            case "Button_Blue":
                buttonText.SetText("Blue Shovel");
                costText.SetText(blueCost + "$");
                dmgText.SetText(blueDMG + " DMG");
                // shovelCost = blueCost;
                // shovelDMG = blueDMG;

                break;
            case "Button_Red":
                buttonText.SetText("Red Shovel");
                costText.SetText(redCost + "$");
                dmgText.SetText(redDMG + " DMG");
                // shovelCost = redCost;
                // shovelDMG = redDMG;

                break;
            case "Button_Green":
                buttonText.SetText("Green Shovel");
                costText.SetText(greenCost + "$");
                dmgText.SetText(greenDMG + " DMG");
                // shovelCost = greenCost;
                // shovelDMG = greenDMG;
                break;
            case "Button_Yellow":
                buttonText.SetText("Yellow Shovel");
                costText.SetText(yellowCost + "$");
                dmgText.SetText(yellowDMG + " DMG");
                // shovelCost = yellorCost;
                // shovelDMG = yellowDMG;
                break;
        }
    }

    public void sendShovelCost()
    {
        UIController uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            switch (gameObject.tag)
            {
                case "Button_Blue":
                    shovelCost = blueCost;
                    break;
                case "Button_Red":
                    shovelCost = redCost;
                    break;
                case "Button_Green":
                    shovelCost = greenCost;
                    break;
                case "Button_Yellow":
                    shovelCost = yellowCost;
                    break;
            }

            uiController.receiveShovelCost(shovelCost);
            Debug.Log("Shovel cost value sent from button: " + shovelCost);
        }
    }

    public int setBlueCost()
    {
        return blueCost;
    }

    public int setRedCost()
    {
        return redCost;
    }

    public int setGreenCost()
    {
        return greenCost;
    }

    public int setYellowCost()
    {
        return yellowCost;
    }

    public void sendDamageValueToLevelGenerator()
    {
        if (lg != null)
        {
            switch (gameObject.tag)
            {
                case "Button_Blue":
                    shovelDMG = blueDMG;
                    break;
                case "Button_Red":
                    shovelDMG = redDMG;
                    break;
                case "Button_Green":
                    shovelDMG = greenDMG;
                    break;
                case "Button_Yellow":
                    shovelDMG = yellowDMG;
                    break;
            }

            //lg.receiveDamageValueFromButton(shovelDMG);
            Debug.Log("Damage value sent from Button script: " + shovelDMG);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}