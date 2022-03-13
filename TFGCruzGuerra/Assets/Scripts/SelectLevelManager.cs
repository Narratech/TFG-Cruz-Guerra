using System.Collections;
using System.Collections.Generic;
using tfg.UI;
using UnityEngine;
using UnityEngine.UI;

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

        public void playLevel(string level)
        {
            GameManager.Instance.level = level;
            sceneChange.changeScene(Scene.Game);
        }
    }
}