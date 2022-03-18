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
        public string Level { 
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
        private bool _tutorial;
        public bool Tutorial { set { _tutorial = value; } }

        public void selected()
        {
            SelectLevelManager.Instance.playLevel(_level,_tutorial);
        }
    }
}