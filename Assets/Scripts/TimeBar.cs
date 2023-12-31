using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeBar : MonoBehaviour
{
    public float duration;
    private float startTime;
    private bool isCounting = false;

    private Vector3 initialScale = new Vector3(0f, 0.775f, 1f);
    private Vector3 targetScale = new Vector3(0.9829f, 0.775f, 1f);

    private RectTransform imageTransform;

    void Start()
    {
        //FindObjectOfType<UIController>().sendDurationToTimeBar();
        
        imageTransform = GetComponent<RectTransform>();
        imageTransform.localScale = initialScale;
        startTime = Time.time;

        //StartCoroutine(ScaleBar());
    }

    private void Update()
    {
        if (duration != 0 && !isCounting)
        {
            isCounting = true;
            StartCoroutine(ScaleBar());
        }
    }

    public void setDuration(float dur)
    {
        duration = dur;
    }


    private IEnumerator ScaleBar()
    {
        Debug.Log("Stage duration: " + duration + "s");
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;

            imageTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            yield return null;
        }

        imageTransform.localScale = targetScale;
    }
}