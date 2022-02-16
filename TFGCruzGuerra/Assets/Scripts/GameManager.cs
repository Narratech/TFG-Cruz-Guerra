using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tfg
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private Scene scene;

        [SerializeField] public LevelManager levelManager;

        public Logic.Table_CompetencesToOB competencesToOB { get; private set; }
        public Logic.Table_OB_Steps OBToSteps { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Instance.scene = scene;

                Instance.levelManager = levelManager;

                Instance.competencesToOB = competencesToOB;
                
                Instance.OBToSteps = OBToSteps;
                Destroy(gameObject);
            }
            else
            {
                OBToSteps = Logic.JsonManager.ImportFromJSON<Logic.Table_OB_Steps>(/*Application.persistentDataPath*/"Assets/GameAssets/Tables/TableOBtoSteps.json");
                competencesToOB = Logic.JsonManager.ImportFromJSON<Logic.Table_CompetencesToOB>(/*Application.persistentDataPath*/"Assets/GameAssets/Tables/TableCompetenceToOB.json");
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void goToScene(ButtonsScene scene)
        {
            SceneManager.LoadScene((int)scene.scene);
        }

        public void exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}