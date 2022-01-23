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

        private void Awake()
        {
            testLevel();
        }

        public void testLevel()
        {
            Logic.Stage stage = Logic.JsonManager.ImportFromJSON<Logic.Stage>(AssetDatabase.GetAssetPath(stageJson));
            Logic.Pilot captain = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(captainJson));
            Logic.Pilot firstOfficer = Logic.JsonManager.ImportFromJSON<Logic.Pilot>(AssetDatabase.GetAssetPath(firstOfficerJson));

            Logic.Table_CompetencesToOB tcob = Logic.JsonManager.ImportFromJSON<Logic.Table_CompetencesToOB>(AssetDatabase.GetAssetPath(tableCompetencesToOBJson));
            Logic.Table_OB_Steps obs = Logic.JsonManager.ImportFromJSON<Logic.Table_OB_Steps>(AssetDatabase.GetAssetPath(tableOBStepsJson), true);

            Logic.Script script = new Logic.Script();

            script.Create(stage, captain, firstOfficer, tcob, obs, null, Logic.Source.Captain);

            script.ExportToJSON("Assets/GameAssets/Scripts/Script1", true);

            script.Play();
        }
    }
}