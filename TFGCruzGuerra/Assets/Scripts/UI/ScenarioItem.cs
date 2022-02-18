using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenarioItem : MonoBehaviour
{
    public float initTime;
    public float endTime;

}
public class EventItem : ScenarioItem
{
    public Logic.Event eventInfo;
}
