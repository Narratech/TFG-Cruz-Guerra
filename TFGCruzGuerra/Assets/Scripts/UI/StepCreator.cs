using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCreator : MonoBehaviour
{
    //hago una clase en lugar de un struct porque los structs son tipos valor, por lo que no son nullable
    private class GeneralData
    {
        public string string1;

        public Logic.PressButton.PressType type;

        public bool loop;

        public GeneralData()
        {
            string1 = "";
            type = Logic.PressButton.PressType.Default;
            loop = false;
        }
    }

    ScenarioItemHandler _scenarioItemHandler;
    PopUpPanel _currentPanel;

    StepItem _editingItem;
    Logic.Step _myStep;
    GeneralData _currentData;

    private void Start()
    {
        _currentData = new GeneralData();
    }

    bool _closed;
    public void setClosed(bool v)
    {
        _closed = v;
    }

    public void setEdit(StepItem item)
    {
        _editingItem = item;

        switch (_editingItem.stepInfo)
        {
            case Logic.Dialog d:
                _currentData.string1 = d.dialog;
                break;
            case Logic.Anim a:
                _currentData.string1 = a.animName;
                break;
            case Logic.PressButton p:
                _currentData.string1 = p.interruptName;
                _currentData.type = p.pressType;
                break;
        }

    }
    public void setScenarioItemHandler(ScenarioItemHandler handler)
    {
        _scenarioItemHandler = handler;
    }

    public void accept()
    {
        if (_scenarioItemHandler && _myStep != null && _myStep.startTime > -1 && _myStep.duration > -1)
        {
            fillStep();
            _scenarioItemHandler.Add(_myStep, this);
            _myStep = null;
            //si estamos editando hay que borrar el antiguo. Lo comprueba la propia funcion
            delete();
        }
        else Debug.LogError("Some values weren't filled, so the step won't be created");

    }

    private void fillStep()
    {
        switch (_myStep)
        {
            case Logic.Dialog d:
                d.dialog = _currentData.string1;
                break;
            case Logic.Anim a:
                a.animName = _currentData.string1;
                break;
            case Logic.PressButton p:
                p.interruptName = _currentData.string1;
                p.pressType = _currentData.type;
                break;
            case Logic.SoundAlarm sa:
                sa.soundAlarmName = _currentData.string1;
                sa.loop = _currentData.loop;
                break;
        }
    }

    public void closeCurrent()
    {
        if (_currentPanel)
            _currentPanel.close();
    }

    public void changePanel(PopUpPanel pan)
    {
        _currentPanel = pan;
        _currentPanel.open();
    }

    public void CreateAnimation()
    {
        if (!_closed)
            _myStep = new Logic.Anim();
    }

    public void CreateDialog()
    {
        if (!_closed)
            _myStep = new Logic.Dialog();
    }

    public void CreatePress()
    {
        if (!_closed)
            _myStep = new Logic.PressButton();
    }

    public void CreateSoundAlarm()
    {
        if (!_closed)
            _myStep = new Logic.SoundAlarm();
    }


    public void setString1(string s)
    {
        if (!_closed)
            _currentData.string1 = s;
    }


    public void setButtonPressType(float type)
    {
        if (!_closed)
        {
            _currentData.type = (Logic.PressButton.PressType)type;
        }
    }


    public void setLoop(float type)
    {
        if (!_closed)
        {
            _currentData.loop = type > 0.5f;
        }
    }

    public void setOB(string ob)
    {
        if (!_closed)
            _myStep.OB = ob;
    }

    public void setResult(float result)
    {
        if (!_closed)
            _myStep.result = (Logic.Step.Result)result;
    }

    public void setDuration(string dur)
    {
        if (!_closed)
            if (_myStep != null)
                if (float.TryParse(dur, out float duration))
                    _myStep.duration = duration;
                else Debug.LogError("Bad format");
    }

    public void setStartTime(string time)
    {
        if (!_closed)
            if (_myStep != null)
                if (float.TryParse(time, out float m_time))
                    _myStep.startTime = m_time;
                else Debug.LogError("Bad format");

    }
    public void delete()
    {
        if (_editingItem)
        {
            GameObject.Destroy(_editingItem.gameObject);
            _editingItem = null;
        }
    }
}
