using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetencesDifficultySelector : MonoBehaviour
{
    public void setCompetence(float value)
    {
        _creator.setCompetence(_competence, value);
    }
    public void setCompetence(string comp) { _competence = comp; }
    public void setCreator(PilotCreator creator) { _creator = creator; }
    string _competence;
    PilotCreator _creator;
}
