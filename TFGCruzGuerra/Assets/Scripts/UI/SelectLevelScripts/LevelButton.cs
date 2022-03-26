using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace tfg.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] Image _backGround;

        private string _level;
        public string Level
        {
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
        public bool Tutorial { get { return _tutorial; } set { _tutorial = value; } }

        public void selected()
        {
            SelectLevelManager.Instance.playLevel(_level, _tutorial);
        }
        public void setBackGroundColor(Color c)
        {
            _backGround.color = c;
        }
    }
}