using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;
using System;
using System.IO;

public class PilotCreator : MonoBehaviour
{
    Pilot _currentPilot;
    ImageModifier _imgModifier;
    [SerializeField] MessagePopUpPanel _panel;
    private void Start()
    {
        newPilot();
    }
    public void SetGender(int gender)
    {
        _currentPilot.Gender = (Pilot.GenderEnum)gender;
    }
    public void setName(string name)
    {
        _currentPilot.Name = name;
        _imgModifier.ReplaceImage();

    }
    public void setAge(string age)
    {
        int nAge;
        if (int.TryParse(age, out nAge))
            setAge(nAge);
    }

    public void setAge(int age)
    {
        if (age > 0)
        {
            _currentPilot.Age = age;
            _imgModifier.ReplaceImage();
        }
    }
    public void setExperience(string exp)
    {
        float nExp;
        if (float.TryParse(exp, out nExp))
            setExperience(nExp);
    }
    public void setExperience(float exp)
    {
        if (exp >= 0 && exp <= 1)
        {
            _currentPilot.Experience = exp;
            _imgModifier.ReplaceImage();
        }
    }
    public void setImgModifier(ImageModifier modi)
    {
        _imgModifier = modi;
    }
    public void newPilot()
    {
        _currentPilot = new Pilot("", -1, -1, Pilot.GenderEnum.None, new Dictionary<string, float>());
    }
    public void save()
    {
        _panel.open();
        if (_currentPilot.Name != "" && _currentPilot.Age > -1 && _currentPilot.Experience > -1
            && _currentPilot.Gender != Pilot.GenderEnum.None)
        {
            //todo revisar ruta
            _panel.setMessage("Pilot saved.");
            string basePath = Application.persistentDataPath + "/Pilots/" + _currentPilot.Name + "/";
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            _currentPilot.ExportToJSON(basePath + _currentPilot.Name + ".json");
        }
        else
        {
            _panel.setMessage("Pilot could not be saved because some fields have incorrect values.");
        }
    }
}
