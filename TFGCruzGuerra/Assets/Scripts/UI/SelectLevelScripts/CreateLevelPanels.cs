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
        private void Start()
        {
            const string path = "Assets/GameAssets/Scripts/";
            DirectoryInfo info = new DirectoryInfo(path);
            IEnumerable<FileInfo> fileInfo = info.GetFiles().Where(name => !name.Name.EndsWith(".meta"));
            foreach (FileInfo file in fileInfo)
            {
                LevelButton go = Instantiate<LevelButton>(prefabLevel, transform);
                string[] name = file.Name.Split('.');
                go.Level = name[1]; 
                go.Tutorial = file.Name.Contains("Tutorial");
                if (go.Tutorial)
                    go.setBackGroundColor(tutorialBackground);
                else go.setBackGroundColor(normalBackground);

            }
        }
    }
}