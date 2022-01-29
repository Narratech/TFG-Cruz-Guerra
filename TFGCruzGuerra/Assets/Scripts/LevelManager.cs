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

        [SerializeField] private TextAsset tableCompetencesToOBJson;

        [SerializeField] private TextAsset tableOBStepsJson;

        private Logic.Script script;

        //Temporal: ideal hacer objetos que traten esto mejor
        [SerializeField] private Image captainImage, firstOfficerImage;
        [SerializeField] private Text captainText, firstOfficerText;

        private void Awake()
        {
            script = new Logic.Script();
            testLevel();
        }

        public void testLevel()
        {
            //Logic.Stage stage = Logic.JsonManager.ImportFromJSON<Logic.Stage>(AssetDatabase.GetAssetPath(stageJson));
            //Logic.Pilot captain = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(captainJson));
            //Logic.Pilot firstOfficer = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(firstOfficerJson));

            //Logic.Table_CompetencesToOB tcob = Logic.JsonManager.ImportFromJSON<Logic.Table_CompetencesToOB>(AssetDatabase.GetAssetPath(tableCompetencesToOBJson));
            //Logic.Table_OB_Steps obs = Logic.JsonManager.ImportFromJSON<Logic.Table_OB_Steps>(AssetDatabase.GetAssetPath(tableOBStepsJson), true);
            //script.Create(stage, captain, firstOfficer, tcob, obs, null, Logic.Source.Captain);

            //script.ExportToJSON("Assets/GameAssets/Scripts/Script1", true);

            //script.Play();
        }

        public void Play()
        {
            StartCoroutine(PlayInCoroutine());
        }

        private IEnumerator PlayInCoroutine()
        {
            float startTime = Time.time;

            Logic.Source sourceNow;
            Logic.Step stepNow;

            Logic.Source sourceNext;
            Logic.Step stepNext;

            script.Next(out sourceNext, out stepNext);

            while (true)
            {
                if (stepNext.startTime > (Time.time - startTime))
                {
                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

                sourceNow = sourceNext;
                stepNow = stepNext;

                captainImage.gameObject.SetActive(false);
                firstOfficerImage.gameObject.SetActive(false);

                switch (stepNow)
                {
                    case Logic.Dialog d:
                        putText(sourceNow, d.dialog);
                        break;
                }

                if(!script.Next(out sourceNext, out stepNext))
                    break;
            }

            captainImage.gameObject.SetActive(false);
            firstOfficerImage.gameObject.SetActive(false);
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
    }
}