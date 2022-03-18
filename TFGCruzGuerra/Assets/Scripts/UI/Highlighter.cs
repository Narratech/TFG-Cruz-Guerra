using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfg.UI
{

    public class Highlighter : MonoBehaviour
    {
        [Serializable]
        struct Highlight
        {
            public Canvas highlightObject;
            public float secondsToWait;
            public float secondsHighlighting;
        }
        [SerializeField] Highlight[] _highlights;
        [SerializeField] GameObject _background;
        private void Start()
        {
            IEnumerator routine;
            foreach (Highlight highlight in _highlights)
            {
                routine = HighlightElement(highlight);

                StartCoroutine(routine);
            }
        }
        IEnumerator HighlightElement(Highlight highlight)
        {
            yield return new WaitForSeconds(highlight.secondsToWait);
            _background.SetActive(true);
            int layer = highlight.highlightObject.sortingOrder;
            highlight.highlightObject.sortingOrder = 1;
            yield return new WaitForSeconds(highlight.secondsHighlighting);
            highlight.highlightObject.sortingOrder = layer;
            _background.SetActive(false);
        }
    }
}
