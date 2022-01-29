using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace tfg
{

    public class OBSelector : MonoBehaviour, IDragHandler, IEndDragHandler
    {

        public void OnDrag(PointerEventData eventData)
        {
            //si esta en el cuarto cuadrante lo pasamos al primero
            float oldAngle = transform.localEulerAngles.z > 270 ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
            float newAngle = oldAngle - eventData.delta.x / _dragDivisor;
            if (Mathf.Abs(newAngle) <= _maxAngle)
            {
                _angle = newAngle;
                transform.localEulerAngles = new Vector3(0, 0, newAngle);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //sabemos que es positivo si el ángulo es negativo y solo evaluamos si el jugador no ha deshecho la eleccion
            if (Mathf.Abs(_angle) > _acceptAngle)
            {
                print(_angle);
                bool accept = _angle < 0;
                _evaluator.evaluate(_OB, accept,eventData.position);

            }
        }
        public void setOB(string OB)
        {
            _OB = OB;
            _modifier.setText(OB);
        }
        public string getOB()
        {
            return _OB;
        }
        string _OB;
        float _angle;
        [SerializeField] TextModifier _modifier;
        [SerializeField] Evaluator _evaluator;
        [SerializeField] float _maxAngle = 21;
        [SerializeField] float _acceptAngle = 12;
        [SerializeField] float _dragDivisor = 2;

    }

}