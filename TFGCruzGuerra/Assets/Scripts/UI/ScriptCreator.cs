using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptCreator : MonoBehaviour
{
    [SerializeField] ScenarioItemHandler _pilot;
    [SerializeField] ScenarioItemHandler _copilot;
    [SerializeField] ScenarioItemHandler _radio;
    string _sceneName = "";
    string _pilotName = "";
    string _copilotName = "";
    public void setSceneName(string sn)
    {
        _sceneName = sn;
    }
    public void setPilotName(string pilot)
    {
        _pilotName = pilot;
    }
    public void setCopilotName(string copilot)
    {
        _copilotName = copilot;
    }
    public void save()
    {
        if (_sceneName != "" && _pilotName != "" && _copilotName != "")
        {
            Logic.Script s = new Logic.Script();
            s.setCaptainName(_pilotName);
            s.setFirstOfficerName(_copilotName);
            s.setSceneName(_sceneName);
            foreach (StepItem item in _pilot.getItems())
            {
                s.addStep(Logic.Source.Captain, item.stepInfo);
            }
            foreach (StepItem item in _copilot.getItems())
            {
                s.addStep(Logic.Source.First_Officer, item.stepInfo);
            }
            foreach (StepItem item in _radio.getItems())
            {
                s.addStep(Logic.Source.Radio, item.stepInfo);
            }
            string directory = Application.persistentDataPath + "/Scripts/";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(Application.persistentDataPath + "/Scripts/");
            s.ExportToJSON(directory + _sceneName + _pilotName + _copilotName + ".json",true);
        }
        else Debug.LogError("Some Names weren't given");
    }
}
