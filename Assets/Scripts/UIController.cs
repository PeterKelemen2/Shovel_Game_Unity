using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI bankText;
    public List<Button> buttonList = new List<Button>();
    private int score = 0;

    
    
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


    public void redClick()
    {
        Debug.Log("Red button clicked");
    }

    public void blueClick()
    {
        Debug.Log("Blue button clicked");
    }

    public void yellowClick()
    {
        Debug.Log("Yellow button clicked");
    }

    public void greenClick()
    {
        Debug.Log("Green button clicked");
    }
    
    public void receiveBlockValue(int value)
    {
        score += value;
        // Debug.Log("Score = " + score);
        setBankText(score);
    }
}