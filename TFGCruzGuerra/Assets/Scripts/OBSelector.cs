using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace tfg
{

    public class OBSelector : MonoBehaviour, IDragHandler, IEndDragHandler
    {

        string _OB;
        float _angle;
        bool _animate;
        [SerializeField] TextModifier _modifier;
        [SerializeField] Evaluator _evaluator;
        [SerializeField] PopUpPanel _myPanel;
        [SerializeField] float _maxAngle = 21;
        [SerializeField] float _rotVel;
        [SerializeField] float _acceptAngle = 12;
        [SerializeField] float _dragDivisor = 2;
        public void OnDrag(PointerEventData eventData)
        {
            _animate = false;
            //si esta en el cuarto cuadrante lo pasamos al primero
            float oldAngle = transform.localEulerAngles.z > 270 ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
            float newAngle = (oldAngle - eventData.delta.x / _dragDivisor);
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
                bool accept = _angle < 0;
                _evaluator.evaluate(_OB, accept, eventData.position);
                transform.localEulerAngles = Vector3.zero;
                _angle = 0;
                _myPanel.close();

            }
            else _animate = true;

        }
        private void Update()
        {
            if (_animate)
            {
                if (Math.Abs(_angle) > 0)
                {
                    _angle -= _rotVel * Math.Sign(_angle) * Time.deltaTime;
                    if ((int)Math.Abs(_angle) == 0)
                        _angle = 0;
                    transform.localEulerAngles = new Vector3(0, 0, _angle);
                }
                else _animate = false;
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

    }

}