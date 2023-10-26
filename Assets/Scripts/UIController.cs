using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public List<Button> buttonList = new List<Button>();
    private int score = 0;

    
    
    void Start()
    {
        scoreText.SetText("Score: " + score);
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
        scoreText.SetText("Score: " + score);
    }
}