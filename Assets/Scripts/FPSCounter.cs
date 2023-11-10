using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    float deltaTime = 0.0f;
    
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    
    void LateUpdate()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{1:0.} fps ({0:0.0} ms)", msec, fps);
    
        // Update the Text component with the new FPS value
        if (fpsText != null)
        {
            fpsText.text = text;
        }
    }
}
