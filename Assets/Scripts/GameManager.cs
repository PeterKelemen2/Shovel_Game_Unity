using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public float timeScale = 0.5f;

    void Start()
    {
        
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
            else
            {
                Time.timeScale = 1.0f; // Restores normal time.
            }
        }
    }
}
