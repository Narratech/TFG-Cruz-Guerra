using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;
using System;
using System.IO;
namespace tfg
{

    public class PilotCreator : MonoBehaviour
    {

        Pilot _currentPilot;
        Table_CompetencesToOB _table;
        [SerializeField] PopUpPanel _panel;
        [SerializeField] TextModifier _panelText;
        private void Start()
        {
            _table = GameManager.Instance.competencesToOB;
            newPilot();
        }
        public void SetGender(int gender)
        {
            _currentPilot.Gender = (Pilot.GenderEnum)gender;
        }
        public void setName(string name)
        {
            _currentPilot.Name = name;

        }
        public void setAge(float age)
        {
            setAge((int)age);
        }

        public void setAge(int age)
        {
            _currentPilot.Age = age;
        }
        public void setExperience(string exp)
        {
            float nExp;
            if (float.TryParse(exp, out nExp))
                setExperience(nExp);
        }
        public void setExperience(float exp)
        {
            _currentPilot.Experience = exp;
        }

        public void newPilot()
        {
            _currentPilot = new Pilot("", -1, -1, Pilot.GenderEnum.None, new Dictionary<string, float>());
        }
        public void save()
        {
            _panel.open();
            if (_currentPilot.Name != "" && _currentPilot.Age > -1 && _currentPilot.Experience > -1
                && _currentPilot.Gender != Pilot.GenderEnum.None && _currentPilot.Competences.Count == _table.getNumCompetences())
            {
                _panelText.setText("Pilot saved.");
                string basePath = Application.persistentDataPath + "/Pilots/" + _currentPilot.Name + "/";
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
                _currentPilot.ExportToJSON(basePath + _currentPilot.Name + ".json");
            }
            else
            {
                _panelText.setText("Pilot could not be saved because some fields have incorrect values.");
            }
        }
        public void setCompetence(string comp, float difficulty)
        {
            _currentPilot.Competences[comp] = difficulty;
        }
    }
}
