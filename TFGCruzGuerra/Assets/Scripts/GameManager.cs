using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public GameManager Instance;

    [SerializeField] private Scene scene;

    private void Awake()
    {
        if(Instance != null)
        {
            Instance.scene = scene;
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
