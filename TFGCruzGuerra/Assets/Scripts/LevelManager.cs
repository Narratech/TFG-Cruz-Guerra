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

        private Logic.Script script;

        //Temporal: ideal hacer objetos que traten esto mejor
        [SerializeField] private Image captainImage, firstOfficerImage;
        [SerializeField] private Text captainText, firstOfficerText;
        Logic.Step _currentStep;

        private void Awake()
        {
            script = new Logic.Script();
            testLevel();
        }

        public void testLevel()
        {
            Logic.Stage stage = Logic.JsonManager.ImportFromJSON<Logic.Stage>(AssetDatabase.GetAssetPath(stageJson));
            Logic.Pilot captain = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(captainJson));
            Logic.Pilot firstOfficer = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(firstOfficerJson));

            script.Create(stage, captain, firstOfficer, GameManager.Instance.competencesToOB, GameManager.Instance.OBToSteps, null, Logic.Source.Captain);

            script.ExportToJSON("Assets/GameAssets/Scripts/Script1", true);

            //script.Play();
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
                            if(!usandose)
                                removeText(nodoAcaba.source);
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

                switch (nodoActual.step)
                {
                    case Logic.Dialog d:
                        putText(nodoActual.source, d.dialog);
                        break;
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

            captainImage.gameObject.SetActive(false);
            firstOfficerImage.gameObject.SetActive(false);

            switch (step)
            {
                case Logic.Dialog d:
                    putText(source, d.dialog);
                    break;
            }
            _currentStep = step;
        }

        private void putText(Logic.Source source, string dialog)
        {
            switch (source)
            {
                case Logic.Source.Captain:
                    captainImage.gameObject.SetActive(true);
                    captainText.text = "captain: " + dialog;
                    Debug.Log(captainText.text);
                    break;
                case Logic.Source.First_Officer:
                    firstOfficerImage.gameObject.SetActive(true);
                    firstOfficerText.text = "first officer: " + dialog;
                    Debug.Log(firstOfficerText.text);
                    break;
                case Logic.Source.Radio:
                    break;
            }
        }

        private void removeText(Logic.Source source)
        {
            switch (source)
            {
                case Logic.Source.Captain:
                    captainImage.gameObject.SetActive(false);
                    Debug.Log("removed captain text image");
                    break;
                case Logic.Source.First_Officer:
                    firstOfficerImage.gameObject.SetActive(false);
                    Debug.Log("removed first officer text image");
                    break;
                case Logic.Source.Radio:
                    break;
            }
        }

        public Logic.Step getCurrentStep() { return _currentStep; }
    }
}