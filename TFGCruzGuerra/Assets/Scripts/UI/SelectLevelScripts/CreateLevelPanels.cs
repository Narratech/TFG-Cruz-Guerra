using Logic;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace tfg.UI
{
    public class CreateLevelPanels : MonoBehaviour
    {
        [SerializeField] private LevelButton prefabLevel;
        [SerializeField] Color normalBackground;
        [SerializeField] Color tutorialBackground;
        [SerializeField] TextAsset[] levels;
        private void Start()
        {

            foreach (TextAsset level in levels)
            {
                LevelButton go = Instantiate<LevelButton>(prefabLevel, transform);
                go.Level = level;
                go.Tutorial = level.name.Contains("Tutorial");
                if (go.Tutorial)
                    go.setBackGroundColor(tutorialBackground);
                else go.setBackGroundColor(normalBackground);

            }
        }
    }
}