using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tfg
{
    public class GameManager : MonoBehaviour
    {
        public struct ResultsData
        {
            public ResultsData(int[] det, int OBs)
            {
                Detection = det;
                TotalOBs = OBs;
            }
            public int[] Detection { get; }
            public int TotalOBs { get; }

        }
        public static GameManager Instance;

        [SerializeField] private Scene scene;

        [SerializeField] public LevelManager levelManager;

        public Logic.Table_CompetencesToOB competencesToOB { get; private set; }
        public Logic.Table_OB_Steps OBToSteps { get; private set; }
        public ResultsData Results { get; set; }
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
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

        }

        public void goToScene(ButtonsScene scene)
        {
            SceneManager.LoadScene((int)scene.scene);
        }

        public void goToSceneAsyncInTime(Scene scene, float inTime)
        {
            StartCoroutine(LoadYourAsyncScene(scene, inTime));
        }

        IEnumerator LoadYourAsyncScene(Scene scene, float inTime)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)scene);
            asyncLoad.allowSceneActivation = false;

            yield return new WaitForSecondsRealtime(inTime);

            asyncLoad.allowSceneActivation = true;

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
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