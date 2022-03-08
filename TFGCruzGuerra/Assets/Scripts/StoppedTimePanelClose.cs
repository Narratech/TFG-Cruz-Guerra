using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace tfg
{
    public class StoppedTimePanelClose : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] Evaluate evaluate;

        public void OnPointerDown(PointerEventData eventData)
        {
            evaluate.stopEvaluating();
        }
    }
}