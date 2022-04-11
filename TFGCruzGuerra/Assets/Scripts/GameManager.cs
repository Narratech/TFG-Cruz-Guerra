using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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

        [SerializeField] private VideoMenuPlayer menuVideo;
        [SerializeField] private Camera menuCam;
        const int maxNumVariants = 5;
        const string defaultVariant = "Joe-0";
        public Logic.Table_CompetencesToOB competencesToOB { get; private set; }
        public Logic.Table_OB_Steps OBToSteps { get; private set; }
        public ResultsData Results { get; set; }

        public TextAsset level { get; set; }

        public float volume { get; set; } = 1;

        public string PilotVariant
        {
            get { return _pilotVariant; }
            set
            {
                string[] variant = value.Split('-');
                //si la variante no esta correctamente separada o si la variante tiene algun error de nombre o de numero
                if (variant.Length != 2 || (variant[0].ToLower() != "joe" && variant[0].ToLower() != "louise") || !int.TryParse(variant[1], out int numVariant) || numVariant >= maxNumVariants || numVariant < 0)
                    _pilotVariant = defaultVariant;
                else _pilotVariant = value;
            }
        }
        public string CoPilotVariant
        {
            get { return _copilotVariant; }
            set
            {
                string[] variant = value.Split('-');
                //si la variante no esta correctamente separada o si la variante tiene algun error de nombre o de numero
                if (variant.Length != 2 || (variant[0].ToLower() != "joe" && variant[0].ToLower() != "louise") || !int.TryParse(variant[1], out int numVariant) || numVariant >= maxNumVariants || numVariant < 0)
                    _copilotVariant = defaultVariant;
                else _copilotVariant = value;
            }
        }
        string _pilotVariant;
        string _copilotVariant;
        private void Awake()
        {
            if (Instance != null)
            {
                Instance.scene = scene;

                Instance.levelManager = levelManager;

                if (scene == Scene.Menu)
                    Instance.menuVideo.start(menuCam);
                else
                    Instance.menuVideo.stop();

                Destroy(gameObject);
            }
            else
            {
                Instance = this;

                menuVideo.prepareVideo();

                if (competencesToOB == null && competencesToOBText != null)
                    competencesToOB = Logic.JsonManager.parseJSON<Logic.Table_CompetencesToOB>(competencesToOBText.ToString());
                if (OBToSteps == null && OBToStepsText != null)
                    OBToSteps = Logic.JsonManager.parseJSON<Logic.Table_OB_Steps>(OBToStepsText.ToString());

                DontDestroyOnLoad(gameObject);
            }


        }

        void Start()
        {
            if (scene == Scene.Loader) goToSceneAsyncInTime(Scene.Menu, 3);
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