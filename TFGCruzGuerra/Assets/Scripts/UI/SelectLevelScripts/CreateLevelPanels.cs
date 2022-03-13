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
        
        private void Start()
        {
            const string path = "Assets/GameAssets/Scripts/";
            DirectoryInfo info = new DirectoryInfo(path);
            IEnumerable<FileInfo> fileInfo = info.GetFiles().Where(name => !name.Name.EndsWith(".meta"));
            foreach (FileInfo file in fileInfo)
            {
                GameObject go = Instantiate<GameObject>(prefabLevel.gameObject, transform);
                go.GetComponent<LevelButton>().level = file.Name.Substring(0, file.Name.Length - 5); // minus ".json"
            }
        }
    }
}