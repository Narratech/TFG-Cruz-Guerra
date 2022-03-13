using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg.UI
{
    public class ButtonSceneChange : MonoBehaviour
    {
        [SerializeField] private Image fadeOutPanel;
        [SerializeField] [Range(0.1f, 1f)] float _secondsToWait = 0.5f;

        public void changeScene(ButtonsScene s)
        {
            GameManager.Instance.goToSceneAsyncInTime(s.scene, _secondsToWait);
            StartCoroutine(fadeOut());
        }

        public void changeScene(Scene s)
        {
            GameManager.Instance.goToSceneAsyncInTime(s, _secondsToWait);
            StartCoroutine(fadeOut());
        }

        private IEnumerator fadeOut()
        {
            Color panelColor = fadeOutPanel.color;
            float fadeAmount;

            float startTime = Time.time;

            while (Time.time - startTime < _secondsToWait)
            {
                fadeAmount = (Time.time - startTime) / _secondsToWait;

                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);

                fadeOutPanel.color = panelColor;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}