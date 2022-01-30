using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace tfg
{

    public class CompetencesDifficultySelector : MonoBehaviour
    {
        string _competence;
        PilotCreator _creator;
        [SerializeField] Slider _slider;
        [SerializeField] Text _text;
        public void setCompetence(float value)
        {
            _creator.setCompetence(_competence, value);
        }
        public void setCompetence(string comp) { _competence = comp; }
        public void setCreator(PilotCreator creator) { _creator = creator; }
        public void reset()
        {
            _slider.value = 0;
            _text.text = "-1";
        }
    }
}
