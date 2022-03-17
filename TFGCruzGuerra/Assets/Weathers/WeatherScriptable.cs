using UnityEngine;

[CreateAssetMenu(fileName = "Weather", menuName = "ScriptableObjects/Weather", order = 1)]
public class WeatherScriptable : ScriptableObject
{
    public string weather;
    public bool rain;
    public Material skyboxMaterial;
}