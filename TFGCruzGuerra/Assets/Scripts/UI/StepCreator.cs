using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCreator : MonoBehaviour
{
    //hago una clase en lugar de un struct porque los structs son tipos valor, por lo que no son nullable
    private class buttonData
    {
        public string name;
        public Logic.PressButton.PressType type;
        public buttonData()
        {
            name = "";
            type = Logic.PressButton.PressType.Default;

        }
    }
    ScenarioItemHandler _scenarioItemHandler;
    Logic.Step _myStep;
    PopUpPanel _currentPanel;
    buttonData _currentButton = null;
    bool _edit = false;
    public void setEdit(bool edit)
    {
        _edit = edit;
    }
    void setScenarioItemHandler(ScenarioItemHandler handler) { _scenarioItemHandler = handler; }
    
    public void accept()
    {
        if (_scenarioItemHandler && _myStep != null && _myStep.OB != "" && _myStep.result != Logic.Step.Result.Neutral)
        {
            _scenarioItemHandler.Add(_myStep);
            _myStep = null;
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
    public void CreateAnimation(string name)
    {
        _myStep = new Logic.Anim(name);
    }
    public void CreateDialog(string d)
    {
        _myStep = new Logic.Dialog(d);
    }
    public void setButtonPressName(string name)
    {
        _myStep = null;
        if (_currentButton == null)
        {

            _currentButton = new buttonData();
            _currentButton.name = name;
        }
        else
        {
            //si hemos cambiado el nombre no hacemos un nuevo boton. Por el contrario, si ya habia algo en el tipo hay que crear el boton
            _currentButton.name = name;
            if (_currentButton.type != Logic.PressButton.PressType.Default)
            {
                _myStep = new Logic.PressButton(_currentButton.name, _currentButton.type);
                _currentButton = null;
            }
        }
    }
    public void setButtonPressType(float type)
    {
        _myStep = null;
        if (_currentButton == null)
        {

            _currentButton = new buttonData();
            _currentButton.type = (Logic.PressButton.PressType)type;
        }
        else
        {
            _currentButton.type = (Logic.PressButton.PressType)type;
            if (_currentButton.name != "")
            {
                _myStep = new Logic.PressButton(_currentButton.name, _currentButton.type);
                _currentButton = null;
            }

        }
    }
    public void setOB(string ob)
    {
        if (_myStep != null)
            _myStep.OB = ob;
        else Debug.LogError("Step is null");
    }
    public void setResult(float result)
    {
        if (_myStep != null)
            _myStep.result = (Logic.Step.Result)result;
        else Debug.LogError("Step is null");
    }
}
