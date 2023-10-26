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


    private int shovelCost;

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


    private void buyShovel(ref bool shovelTypeOwned)
    {
        if (!shovelTypeOwned)
        {
            Debug.Log("Shovel type not owned. Buying...");
            if (score > shovelCost)
            {
                shovelTypeOwned = true;
                score -= shovelCost;
                ButtonScript button = FindObjectOfType<ButtonScript>();
                if (button != null)
                {
                    button.sendDamageValueToLevelGenerator();
                }
                Debug.Log("Shovel type bought");
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        else
        {
            Debug.Log("Shovel type already owned");
        }
    }

    public void redClick()
    {
        Debug.Log("Red button clicked");
        buyShovel(ref redOwned);
        //receiveShovelCost(shovelCost);
    }

    public void blueClick()
    {
        Debug.Log("Blue button clicked");
        buyShovel(ref blueOwned);
    }

    public void yellowClick()
    {
        Debug.Log("Yellow button clicked");
        buyShovel(ref yellowOwned);
    }

    public void greenClick()
    {
        Debug.Log("Green button clicked");
        buyShovel(ref greenOwned);
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