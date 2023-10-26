using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Button : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI dmgText;

    private int blueDMG = 1;
    private int redDMG = 2;
    private int greenDMG = 3;
    private int yellowDMG = 4;

    private int blueCost = 1000;
    private int redCost = 2000;
    private int greenCost = 3000;
    private int yellorCost = 4000;
    
    
    
    void Start()
    {
        switch (gameObject.tag)
        {
            case "Button_Blue":
                buttonText.SetText("Blue Shovel");
                costText.SetText(blueCost + "$");
                dmgText.SetText(blueDMG + " DMG");
                break;
            case "Button_Red":
                buttonText.SetText("Red Shovel");
                costText.SetText(redCost + "$");
                dmgText.SetText(redDMG + " DMG");
                break;
            case "Button_Green":
                buttonText.SetText("Green Shovel");
                costText.SetText(greenCost + "$");
                dmgText.SetText(greenDMG + " DMG");
                break;
            case "Button_Yellow":
                buttonText.SetText("Yellow Shovel");
                costText.SetText(yellorCost + "$");
                dmgText.SetText(yellowDMG + " DMG");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
