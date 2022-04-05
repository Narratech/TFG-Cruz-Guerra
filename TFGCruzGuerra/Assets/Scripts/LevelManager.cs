using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace tfg
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TextAsset stageJson;
        [SerializeField] private TextAsset captainJson;
        [SerializeField] private TextAsset firstOfficerJson;

        [SerializeField] private Image stoppedTimePanel;

        [SerializeField] private Image fadeOutPanel;
        [SerializeField] private float waitStart = 1.0f;
        private Logic.Script script;

        Logic.Step _currentStep;
        static List<Interfaces.INewStepHandler> newStepHandlers;
        static List<Interfaces.IEndStepHandler> endStepHandlers;

        [SerializeField] Scene _resultsScene;
        [SerializeField] [Tooltip("Seconds to wait until scene changes")] float _secondsToWait = .5f;

        private IEnumerator playCoroutine;

        private void Awake()
        {
            if (newStepHandlers == null)
                newStepHandlers = new List<Interfaces.INewStepHandler>();
            if (endStepHandlers == null)
                endStepHandlers = new List<Interfaces.IEndStepHandler>();
            script = Logic.JsonManager.parseJSON<Logic.Script>(GameManager.Instance.level.ToString(), true);
            Play();
        }

        public static void AddNewStepHandler(Interfaces.INewStepHandler h) { newStepHandlers.Add(h); }
        public static void RemoveNewStepHandler(Interfaces.INewStepHandler h) { newStepHandlers.Remove(h); }
        public static void AddEndStepHandler(Interfaces.IEndStepHandler h) { endStepHandlers.Add(h); }
        public static void RemoveEndStepHandler(Interfaces.IEndStepHandler h) { endStepHandlers.Remove(h); }

        private void Play()
        {
            playCoroutine = PlayInCoroutine();
            StartCoroutine(playCoroutine);
        }

        private IEnumerator PlayInCoroutine()
        {
            float startTime = Time.time;
            Utils.ColaPrioridad colaStarts = null;
            Utils.ColaPrioridad colaEnds = null;
            //Nunca debe haber un nodo en ambas colas a la vez
            try
            {
                colaStarts = new Utils.ColaPrioridad(script.NumberOfSteps(), Utils.Nodo.CompareStartTime);
                colaEnds = new Utils.ColaPrioridad(script.NumberOfSteps(), Utils.Nodo.CompareEndTime);
            }
            catch (Exception e)
            {
                Debug.LogWarning("EL ERROR ES " + e.Message);
            }
            Logic.Source sourceNow;
            Logic.Step stepNow;

            while (script.Next(out sourceNow, out stepNow))
            {
                colaStarts.Introducir(new Utils.Nodo(sourceNow, stepNow));
            }

            yield return new WaitForSeconds(Math.Max(0, waitStart - (Time.time - startTime)));

            startTime = Time.time;
            Utils.Nodo nodoSiguiente = colaStarts.ObservarPrimero();
            while (true)
            {
                float timeElapsed = Time.time - startTime;

                //Check si hay algun paso ha acabado
                if (colaEnds.NumeroElementos() > 0 && timeElapsed > colaEnds.ObservarPrimero().endTime)
                {
                    Utils.Nodo nodoAcaba = colaEnds.EliminarPrimero();

                    //Solo quito el texto si no hay nadie usandolo
                    bool usandose = false;
                    if (nodoAcaba.step is Logic.Dialog)
                    {
                        for (int i = 0; i < colaEnds.NumeroElementos(); ++i)
                        {
                            //Si alguien lo usa
                            if (colaEnds.Array()[i].step is Logic.Dialog && colaEnds.Array()[i].source == nodoAcaba.source)
                            {
                                usandose = true;
                                break;
                            }
                        }
                    }
                    if (!usandose)
                    {
                        //todo si en el resto de tipos tenemos que gestionar el uso de forma especial tambien hay que añadir este foreach a cada uno
                        foreach (Interfaces.IEndStepHandler handler in endStepHandlers)
                        {
                            handler.OnEndStep(nodoAcaba.step, nodoAcaba.source, colaStarts.NumeroElementos());
                        }
                    }

                    //Si no quedan pasos que meter ni pasos que acaben, acabo el play
                    if (nodoSiguiente == null && colaEnds.NumeroElementos() == 0)
                        break;
                }

                // tiempo pasado < tiempo de inicio -> hay que esperar mas
                if (nodoSiguiente == null || timeElapsed < nodoSiguiente.startTime)
                {
                    //Esperar 
                    yield return new WaitForSeconds(0.10f);
                    continue;
                }

                //Un nuevo evento que hay que poner
                Utils.Nodo nodoActual = colaStarts.EliminarPrimero();
                colaEnds.Introducir(nodoActual);
                foreach (Interfaces.INewStepHandler handler in newStepHandlers)
                {
                    handler.OnNewStep(nodoActual.step, nodoActual.source, colaStarts.NumeroElementos());
                }
                if (colaStarts.NumeroElementos() > 0)
                    nodoSiguiente = colaStarts.ObservarPrimero();
                else
                    nodoSiguiente = null;
            }

            GameManager.Instance.goToSceneAsyncInTime(_resultsScene, _secondsToWait);
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

        public void nextStep()
        {
            Logic.Source source;
            Logic.Step step;
            script.Next(out source, out step);

            //captainImage.gameObject.SetActive(false);
            //firstOfficerImage.gameObject.SetActive(false);

            switch (step)
            {
                case Logic.Dialog d:
                    //putText(source, d.dialog);
                    break;
            }
            _currentStep = step;
        }

        public void setScaleTime(float t)
        {
            Time.timeScale = t;

            if (stoppedTimePanel == null)
                return;

            if (t <= 0.01)
                stoppedTimePanel.enabled = true;
            else
                stoppedTimePanel.enabled = false;
        }

        public Logic.Step getCurrentStep() { return _currentStep; }

        public void abort()
        {
            StopCoroutine(playCoroutine);

            GameManager.Instance.goToSceneAsyncInTime(_resultsScene, _secondsToWait);

            setScaleTime(1);

            StartCoroutine(fadeOut());
        }
    }
}