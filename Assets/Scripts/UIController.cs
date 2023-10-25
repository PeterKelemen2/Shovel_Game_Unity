using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject player;

    private int score = 0;


    void Start()
    {
        scoreText.SetText("Score: " + score);
    }


    void Update()
    {
    }

    public void receiveBlockValue(int value)
    {
        score += value;
        // Debug.Log("Score = " + score);
        scoreText.SetText("Score: " + score);
    }
}