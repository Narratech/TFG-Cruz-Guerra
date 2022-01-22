using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
public class PilotCreator : MonoBehaviour
{
    Pilot _currentPilot;
    ImageModifier _imgModifier;
    private void Start()
    {
        _currentPilot = ScriptableObject.CreateInstance<Pilot>();
        _currentPilot.Age = -1;
        _currentPilot.Name = "";
        _currentPilot.Experience = -1;
    }
    public void SetGender(Pilot.GenderEnum gender)
    {
        _currentPilot.Gender = gender;
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
}
