using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepItem : ScenarioItem
{
    public Logic.Step stepInfo;
    public PopUpPanel Panel { get; set; }
    public StepCreator Creator { get; set; }
    public void edit()
    {
        Creator.setEdit(this);
        Panel.open();
    }
}

