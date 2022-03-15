using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace tfg
{

    public class OBSelector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        string _OB;
        float _angle;
        bool _animate;
        bool _mistaken = false;
        [SerializeField] TextModifier _modifier;
        [SerializeField] Evaluator _evaluator;
        [SerializeField] PopUpPanel _myPanel;
        [SerializeField] float _maxAngle = 21;
        [SerializeField] float _rotVel;
        [SerializeField] float _acceptAngle = 12;
        [SerializeField] float _dragDivisor = 2;
        public UnityEvent OnTutorialMistake { get; set; }
        public bool Tutorial { get; set; }

        float _startDragPoint;

        public void OnDrag(PointerEventData eventData)
        {
            _animate = false;

            float relativePosX = _startDragPoint - eventData.position.x;

            float newAngle = relativePosX / _dragDivisor;

            if (newAngle >= _maxAngle) newAngle = _maxAngle;
            else if (newAngle < -_maxAngle) newAngle = -_maxAngle;
            //le dejamos girar si no es el tutorial o bien si es el tutorial y va a acertar
            if (!Tutorial || Math.Abs(newAngle) < _acceptAngle ||
                (Tutorial && _evaluator.evaluate(new Evaluator.EvaluableInfo(_OB,
                newAngle < 0 ? Logic.Step.Result.Good : Logic.Step.Result.Bad))))
            {
                _angle = newAngle;
                transform.localEulerAngles = new Vector3(0, 0, _angle);
            }
            //si ya nos hemos equivocado no volvemos a tirar el evento
            else if (!_mistaken)
            {
                OnTutorialMistake?.Invoke();
                _mistaken = true;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //sabemos que es positivo si el ángulo es negativo y solo evaluamos si el jugador no ha deshecho la eleccion
            if (Mathf.Abs(_angle) > _acceptAngle)
            {
                bool accept = _angle < 0;
                _evaluator.evaluate(_OB, accept);
                transform.localEulerAngles = Vector3.zero;
                _angle = 0;
                _myPanel.close();

            }
            else
            {
                _animate = true;
                _mistaken = false;
            }
        }

        private void Update()
        {
            if (_animate)
            {
                if (Math.Abs(_angle) > 0)
                {
                    _angle -= _rotVel * Math.Sign(_angle) * Time.unscaledDeltaTime;
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startDragPoint = eventData.position.x;
        }
    }

}