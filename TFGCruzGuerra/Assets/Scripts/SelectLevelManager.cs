using System.Collections;
using System.Collections.Generic;
using tfg.UI;
using UnityEngine;

namespace tfg
{
    public class SelectLevelManager : MonoBehaviour
    {
        public static SelectLevelManager Instance;

        [SerializeField] private ButtonSceneChange sceneChange;

        private void Awake()
        {
            Instance = this;
        }

        public void playLevel(TextAsset level,bool tutorial)
        {
            GameManager.Instance.level = level;
            if (tutorial)
                sceneChange.changeScene(Scene.Tutorial);
            else
            sceneChange.changeScene(Scene.Game);
        }
    }
}