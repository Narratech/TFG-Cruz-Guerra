using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg.UI
{
    public class ButtonSceneChange : MonoBehaviour
    {
        [SerializeField] private Image fadeOutInPanel;
        [SerializeField] [Range(0.1f, 1f)] float _secondsToWait = 0.5f;
        [SerializeField] [Range(0f, 1f)] float _secondsToAppear = 0.5f;

        private void Start()
        {
            if (fadeOutInPanel != null && _secondsToAppear > 0.01f) StartCoroutine(fadeIn());
        }

        public void changeScene(ButtonsScene s)
        {
            GameManager.Instance.goToSceneAsyncInTime(s.scene, _secondsToWait);
            if(fadeOutInPanel != null) StartCoroutine(fadeOut());
        }

        public void changeScene(Scene s)
        {
            GameManager.Instance.goToSceneAsyncInTime(s, _secondsToWait);
            if (fadeOutInPanel != null) StartCoroutine(fadeOut());
        }

        private IEnumerator fadeOut()
        {
            Color panelColor = fadeOutInPanel.color;
            float fadeAmount;

            float startTime = Time.time;

            while (Time.time - startTime < _secondsToWait)
            {
                fadeAmount = (Time.time - startTime) / _secondsToWait;

                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);

                fadeOutInPanel.color = panelColor;

                yield return new WaitForEndOfFrame();
            }
            fadeOutInPanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, 1);
        }

        private IEnumerator fadeIn()
        {
            Color panelColor = fadeOutInPanel.color;
            float fadeAmount;

            float startTime = Time.time;

            while (Time.time - startTime < _secondsToAppear)
            {
                fadeAmount = (Time.time - startTime) / _secondsToAppear;

                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, 1-fadeAmount);

                fadeOutInPanel.color = panelColor;

                yield return new WaitForEndOfFrame();
            }
            fadeOutInPanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);

        }
    }
}