using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    private float timeScale = 4.0f;
    private int targetFPS = 1000;

    void Start()
    {
        Application.targetFrameRate = targetFPS;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Toggle between slowing and normal time by changing timeScale.
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = timeScale; // Slows down time.
            }
            else if(Time.timeScale == timeScale)
            {
                Time.timeScale = 1.0f; // Restores normal time.
            }
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
