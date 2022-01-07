using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tfg
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public GameManager Instance;

        [SerializeField] private Scene scene;

        [SerializeField] private LevelManager levelManager;

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