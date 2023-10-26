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

    public int shovelCost = 0;
    public int shovelDMG = 0;

    public bool blueOwned = false;
    public bool redOwned = false;
    public bool greenOwned = false;
    public bool yellowOwned = false;
    
    void Start()
    {
        switch (gameObject.tag)
        {
            case "Button_Blue":
                buttonText.SetText("Blue Shovel");
                costText.SetText(blueCost + "$");
                dmgText.SetText(blueDMG + " DMG");
                shovelCost = blueCost;
                shovelDMG = blueDMG;
                break;
            case "Button_Red":
                buttonText.SetText("Red Shovel");
                costText.SetText(redCost + "$");
                dmgText.SetText(redDMG + " DMG");
                shovelCost = redCost;
                shovelDMG = redDMG;
                break;
            case "Button_Green":
                buttonText.SetText("Green Shovel");
                costText.SetText(greenCost + "$");
                dmgText.SetText(greenDMG + " DMG");
                shovelCost = greenCost;
                shovelDMG = greenDMG;
                break;
            case "Button_Yellow":
                buttonText.SetText("Yellow Shovel");
                costText.SetText(yellorCost + "$");
                dmgText.SetText(yellowDMG + " DMG");
                shovelCost = yellorCost;
                shovelDMG = yellowDMG;
                break;
        }
    }
    
    public void sendShovelCost()
    {
        UIController uiController = FindObjectOfType<UIController>();
        if (uiController != null)
        {
            uiController.receiveShovelCost(shovelCost);
            Debug.Log("Shovel cost value sent from button: " + shovelCost);
        }
    }

    public void sendDamageTaken()
    {
        Block block = FindObjectOfType<Block>();
        if (block != null)
        {
            block.receiveDamageTaken(shovelDMG);
        }
    }

    public void sendDamageValueToLevelGenerator()
    {
        LevelGenerator2 lg = FindObjectOfType<LevelGenerator2>();
        if (lg != null)
        {
            lg.receiveDamageValueFromButton(shovelDMG);
            Debug.Log("Damage value sent from Button script: " + shovelDMG);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
