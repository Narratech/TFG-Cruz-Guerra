using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace tfg
{
    public class StoppedTimePanelClose : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] Evaluate evaluate;
        public bool CanExit { get; set; } = true;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (CanExit)
                evaluate.stopEvaluating();
        }

        public void enableExit()
        {
            CanExit = false;
        }
        public void DisableExit()
        {
            CanExit = true;
        }
    }
}