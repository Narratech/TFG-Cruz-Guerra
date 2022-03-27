using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg
{
    public class Flicker : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField] private float seconds;

        private void OnEnable()
        {
            StartCoroutine(fadeIn());
        }

        private IEnumerator fadeIn()
        {
            Color panelColor = img.color;
            float fadeAmount;

            float startTime = Time.unscaledTime;

            while (Time.unscaledTime - startTime < seconds)
            {
                fadeAmount = (Time.unscaledTime - startTime) / seconds;

                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);

                img.color = panelColor;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}