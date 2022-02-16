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

        private Logic.Script script;

        Logic.Step _currentStep;
        static List<Interfaces.INewStepHandler> newStepHandlers;
        static List<Interfaces.IEndStepHandler> endStepHandlers;

        private void Awake()
        {
            if (newStepHandlers == null)
                newStepHandlers = new List<Interfaces.INewStepHandler>();
            if (endStepHandlers == null)
                endStepHandlers = new List<Interfaces.IEndStepHandler>();

            script = new Logic.Script();
            testLevel();
        }

        public static void AddNewStepHandler(Interfaces.INewStepHandler h) { newStepHandlers.Add(h); }
        public static void RemoveNewStepHandler(Interfaces.INewStepHandler h) { newStepHandlers.Remove(h); }
        public static void AddEndStepHandler(Interfaces.IEndStepHandler h) { endStepHandlers.Add(h); }
        public static void RemoveEndStepHandler(Interfaces.IEndStepHandler h) { endStepHandlers.Remove(h); }
        public void testLevel()
        {
            Logic.Stage stage = Logic.JsonManager.ImportFromJSON<Logic.Stage>(AssetDatabase.GetAssetPath(stageJson));
            Logic.Pilot captain = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(captainJson));
            Logic.Pilot firstOfficer = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(firstOfficerJson));

            script.Create(stage, captain, firstOfficer, GameManager.Instance.competencesToOB, GameManager.Instance.OBToSteps, null, Logic.Source.Captain);

            //script = Logic.JsonManager.ImportFromJSON<Logic.Script>("Assets/GameAssets/Scripts/Script2", true);
        }

        public void Play()
        {
            StartCoroutine(PlayInCoroutine());
        }
        
        private IEnumerator PlayInCoroutine()
        {
            float startTime = Time.time;

            //Nunca debe haber un nodo en ambas colas a la vez
            Utils.ColaPrioridad colaStarts = new Utils.ColaPrioridad(script.NumberOfSteps(), Utils.Nodo.CompareStartTime);
            Utils.ColaPrioridad colaEnds = new Utils.ColaPrioridad(script.NumberOfSteps(), Utils.Nodo.CompareEndTime);

            Logic.Source sourceNow;
            Logic.Step stepNow;

            while(script.Next(out sourceNow, out stepNow))
            {
                colaStarts.Introducir(new Utils.Nodo(sourceNow, stepNow));
            }

            Utils.Nodo nodoSiguiente = colaStarts.ObservarPrimero();
            while (true)
            {
                float timeElapsed = Time.time - startTime;

                //Check si hay algun paso ha acabado
                if(colaEnds.NumeroElementos() > 0 && timeElapsed > colaEnds.ObservarPrimero().endTime)
                {
                    Utils.Nodo nodoAcaba = colaEnds.EliminarPrimero();

                    switch (nodoAcaba.step)
                    {
                        //Solo quito el texto si no hay nadie usandolo
                        case Logic.Dialog d:
                            bool usandose = false;
                            for(int i = 0; i < colaEnds.NumeroElementos(); ++i)
                            {
                                //Si alguien lo usa
                                if(colaEnds.Array()[i].step is Logic.Dialog && colaEnds.Array()[i].source == nodoAcaba.source)
                                {
                                    usandose = true;
                                    break;
                                }
                            }
                            if (!usandose)
                            {
                                //todo si en el resto de tipos tenemos que gestionar el uso de forma especial tambien hay que añadir este foreach a cada uno
                                foreach (Interfaces.IEndStepHandler handler in endStepHandlers)
                                {
                                    handler.EndStep(nodoAcaba.step, nodoAcaba.source);
                                }
                            }
                            break;
                    }

                    //Si no quedan pasos que meter ni pasos que acaben, acabo el play
                    if (nodoSiguiente == null && colaEnds.NumeroElementos() == 0)
                        break;
                }

                // tiempo pasado < tiempo de inicio -> hay que esperar mas
                if(nodoSiguiente == null || timeElapsed < nodoSiguiente.startTime)
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
                    handler.NewStep(nodoActual.step, nodoActual.source);
                }
                if (colaStarts.NumeroElementos() > 0)
                    nodoSiguiente = colaStarts.ObservarPrimero();
                else
                    nodoSiguiente = null;
            }

            yield return null;
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

        //Called from button
        public void setScaleTime(float t)
        {
            Time.timeScale = t;
            if (t <= 0.01) 
                stoppedTimePanel.enabled = true;
            else 
                stoppedTimePanel.enabled = false;
        }

        public Logic.Step getCurrentStep() { return _currentStep; }
    }
}