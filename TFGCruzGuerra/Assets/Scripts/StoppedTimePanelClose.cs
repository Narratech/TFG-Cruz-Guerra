using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace tfg
{
    public class StoppedTimePanelClose : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] PopUpPanel evaluator;

        public void OnPointerDown(PointerEventData eventData)
        {
            evaluator.close();
            GameManager.Instance.levelManager.setScaleTime(1);
        }
    }
}