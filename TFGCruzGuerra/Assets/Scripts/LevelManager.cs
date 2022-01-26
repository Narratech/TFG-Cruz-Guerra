using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

        string dialog;

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

            Logic.Table_CompetencesToOB tcob = Logic.JsonManager.ImportFromJSON<Logic.Table_CompetencesToOB>(AssetDatabase.GetAssetPath(tableCompetencesToOBJson));
            Logic.Table_OB_Steps obs = Logic.JsonManager.ImportFromJSON<Logic.Table_OB_Steps>(AssetDatabase.GetAssetPath(tableOBStepsJson), true);
            script.Create(stage, captain, firstOfficer, tcob, obs, null, Logic.Source.Captain);

            script.ExportToJSON("Assets/GameAssets/Scripts/Script1", true);

            //script.Play();
        }

        public void nextStep()
        {
            Logic.Source source;
            Logic.Step step;
            script.Next(out source, out step);

            switch (step)
            {
                case Logic.Dialog d:
                    Debug.Log("From " + source.ToString() + ": " + d.dialog);
                    break;
            }
        }
    }
}