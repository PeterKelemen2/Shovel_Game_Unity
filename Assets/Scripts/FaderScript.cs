using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class FaderScript : MonoBehaviour
{
    private float fadeTime = 0.3f;
    private bool isFadedOut = false;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();


        Debug.Log("Image: " + buttonImage);
        Debug.Log("buttonText: " + buttonText);
    }

    public void fadeOut()
    {
        //Invoke("setInteractableFalse", fadeTime);
        gameObject.SetActive(false);
        buttonImage.CrossFadeAlpha(0f, fadeTime, false);
        buttonText.CrossFadeAlpha(0f, fadeTime, false);
    }

    public void fadeIn()
    {
        gameObject.SetActive(true);
        //Invoke("setInteractableTrue", fadeTime);
        buttonImage.CrossFadeAlpha(1f, fadeTime, false);
        buttonText.CrossFadeAlpha(1f, fadeTime * 1.3f, false);
    }

    private void setInteractableFalse()
    {
        gameObject.SetActive(false);
    }

    private void setInteractableTrue()
    {
        gameObject.SetActive(true);
    }
}