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
            public string name;
            public Canvas highlightObject;
        }
        [SerializeField] Highlight[] _highlights;
        [SerializeField] GameObject _background;

        public void highLight(string h, bool a)
        {
            foreach(Highlight hl in _highlights)
            {
                if(hl.name == h)
                {
                    highLight(hl, a);
                }
            }
        }

        private void highLight(Highlight h, bool a)
        {
            _background.SetActive(a);
            h.highlightObject.sortingOrder = a ? 100 : 1;
        }
    }
}
