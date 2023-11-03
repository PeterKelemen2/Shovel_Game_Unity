using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class FadePanelClass : MonoBehaviour
    {
        public Image panel;
        public float fadeTime;
        private Color colorToFadeTo = new Color(1f, 1f, 1f, 0f);


        void Start()
        {
            panel.CrossFadeColor(colorToFadeTo, fadeTime, true, true);
            StartCoroutine(disableAfterFade(fadeTime));
        }

        IEnumerator disableAfterFade(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}