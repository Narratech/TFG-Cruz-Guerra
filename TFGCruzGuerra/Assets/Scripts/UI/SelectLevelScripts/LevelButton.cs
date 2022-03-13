using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace tfg.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelName;

        private string _level;
        public string level { 
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                levelName.text = _level;
            }
        }

        public void selected()
        {
            SelectLevelManager.Instance.playLevel(_level);
        }
    }
}