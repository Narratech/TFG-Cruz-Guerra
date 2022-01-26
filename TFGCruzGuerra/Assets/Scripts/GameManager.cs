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

        [SerializeField] public LevelManager levelManager { get; private set; }

        private void Awake()
        {
            if(Instance != null)
            {
                Instance.scene = scene;

                Instance.levelManager = levelManager;

                Destroy(gameObject);
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