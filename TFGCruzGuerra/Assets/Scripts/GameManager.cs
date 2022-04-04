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

        [SerializeField] private TextAsset competencesToOBText;
        [SerializeField] private TextAsset OBToStepsText;

        public Logic.Table_CompetencesToOB competencesToOB { get; private set; }
        public Logic.Table_OB_Steps OBToSteps { get; private set; }
        public ResultsData Results { get; set; }

        public TextAsset level { get; set; }

        public float volume { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Instance.scene = scene;

                Instance.levelManager = levelManager;

                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                if (competencesToOB == null && competencesToOBText != null)
                    competencesToOB = Logic.JsonManager.parseJSON<Logic.Table_CompetencesToOB>(competencesToOBText.ToString());
                if (OBToSteps == null && OBToStepsText != null)
                    OBToSteps = Logic.JsonManager.parseJSON<Logic.Table_OB_Steps>(OBToStepsText.ToString());

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