using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FadePausePanel : MonoBehaviour
    {
        private Image panel;
        private const float fadeTime = 0.1f;
        private Color colorToFadeTo = new Color(0.22f, 0.17f, 0.18f, 0.8f);
        private Color toAlphaColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        void Start()
        {
            gameObject.SetActive(true);
            panel = GetComponentInChildren<Image>();
        }

        public void setResumeColor(float fadeParam = fadeTime)
        {
            panel.CrossFadeColor(toAlphaColor, fadeParam, true, true);
            StartCoroutine(disableAfterSeconds(fadeParam));
        }

        public void setPauseColor(float fadeParam = fadeTime)
        {
            gameObject.SetActive(true);
            panel.CrossFadeColor(colorToFadeTo, fadeParam, true, true);
        }

        private IEnumerator disableAfterSeconds(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}