using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI bankText;
    public List<Button> buttonList = new();
    private int score = 0;


    private bool blueOwned = false;
    private bool redOwned = false;
    private bool greenOwned = false;
    private bool yellowOwned = false;

    private int blueDMG = 1;
    private int redDMG = 2;
    private int greenDMG = 3;
    private int yellowDMG = 4;

    private int blueCost = 10;
    private int redCost = 10;
    private int greenCost = 10;
    private int yellowCost = 4000;


    private int shovelCost;
    public int currentDamage = 1;

    void Start()
    {
        setBankText(score);
    }

    private void setBankText(int score)
    {
        bankText.SetText("Bank: " + score + "$");
    }


    void Update()
    {
    }

    private void setDamageOf(String shovelName)
    {
        switch (shovelName)
        {
            case "Button_Blue":
                currentDamage = blueDMG;
                break;
            case "Button_Red":
                currentDamage = redDMG;
                break;
            case "Button_Green":
                currentDamage = greenDMG;
                break;
            case "Button_Yellow":
                currentDamage = yellowDMG;
                break;
        }
    }

    private int getShovelCostOf(String shovelName)
    {
        switch (shovelName)
        {
            case "Button_Blue":
                return blueCost;
            case "Button_Red":
                return redCost;
            case "Button_Green":
                return blueCost;
            case "Button_Yellow":
                return yellowCost;
            default:
                return 0;
        }
    }

    private void buyShovel(ref bool shovelTypeOwned, String shovelName)
    {
        if (!shovelTypeOwned)
        {
            Debug.Log(shovelName.Substring(7) + " shovel not owned. Attempting to buy...");
            if (score >= getShovelCostOf(shovelName))
            {
                shovelTypeOwned = true;
                score -= getShovelCostOf(shovelName);
                setBankText(score);
                setDamageOf(shovelName);
                sendDamageToLevelGenerator();

                Debug.Log(shovelName.Substring(7) + " shovel bought");
            }
            else
            {
                Debug.Log("Not enough money for " + shovelName.Substring(7) + " shovel");
            }
        }
        else
        {
            Debug.Log(shovelName.Substring(7) + " shovel already owned, setting its damage");
            setDamageOf(shovelName);
            sendDamageToLevelGenerator();
            Debug.Log("Damage of " + currentDamage + " given to Level Generator");
        }
    }


    public void sendDamageToLevelGenerator()
    {
        LevelGenerator2 lg = FindObjectOfType<LevelGenerator2>();
        if (lg != null)
        {
            lg.receiveDamageValueFromUI(currentDamage);
        }
    }

    public void redClick()
    {
        Debug.Log("Red button clicked");
        buyShovel(ref redOwned, "Button_Red");
        //receiveShovelCost(shovelCost);
    }

    public void blueClick()
    {
        Debug.Log("Blue button clicked");
        buyShovel(ref blueOwned, "Button_Blue");
    }

    public void yellowClick()
    {
        Debug.Log("Yellow button clicked");
        buyShovel(ref yellowOwned, "Button_Yellow");
    }

    public void greenClick()
    {
        Debug.Log("Green button clicked");
        buyShovel(ref greenOwned, "Button_Green");
    }

    public void receiveBlockValue(int value)
    {
        score += value;
        // Debug.Log("Score = " + score);
        setBankText(score);
    }

    public void receiveShovelCost(int cost)
    {
        shovelCost = cost;
        Debug.Log("Shovel cost received from button: " + shovelCost);
    }
}