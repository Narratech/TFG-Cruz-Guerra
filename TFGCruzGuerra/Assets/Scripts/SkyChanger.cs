using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{

    [SerializeField] ParticleSystem rainParticlesEmission;

    [SerializeField] Material camSkybox;

    [SerializeField] Material sunny, rainy;

    [SerializeField] float blendDuration = 1f;

    [SerializeField] WeatherScriptable[] weathers;

    private string actualWeather;

    private Dictionary<string, Material> weatherToSkyBox;
    private Dictionary<string, bool> weatherToRainy;

    private void Start()
    {
        weatherToSkyBox = new Dictionary<string, Material>();
        weatherToRainy = new Dictionary<string, bool>();
        foreach(WeatherScriptable w in weathers)
        {
            weatherToSkyBox.Add(w.weather, w.skyboxMaterial);
            weatherToRainy.Add(w.weather, w.rain);
        }

        changeWeather(weathers[0].weather);
        actualWeather = weathers[0].weather;
    }

    public void changeWeather(string weather)
    {
        if (!weatherToRainy.ContainsKey(weather) && weather == actualWeather)
            return;

        actualWeather = weather;

        ParticleSystem.EmissionModule a = rainParticlesEmission.emission;
        a.enabled = weatherToRainy[weather];

        StartCoroutine(blend(weatherToSkyBox[weather]));
    }

    private IEnumerator blend(Material to)
    {
        setTexture2(to);

        float startTime = Time.time;

        while(Time.time < startTime + blendDuration)
        {
            float lerp = (Time.time - startTime) / blendDuration;
            camSkybox.SetFloat("_Blend", lerp);
            yield return new WaitForEndOfFrame();
        }

        setTexture1(to);

        camSkybox.SetFloat("_Blend", 0);
    }

    private void setTexture1(Material skybox1)
    {
        camSkybox.SetTexture("_FrontTex", skybox1.GetTexture("_FrontTex"));
        camSkybox.SetTexture("_BackTex", skybox1.GetTexture("_BackTex"));

        camSkybox.SetTexture("_UpTex", skybox1.GetTexture("_UpTex"));
        camSkybox.SetTexture("_DownTex", skybox1.GetTexture("_DownTex"));

        camSkybox.SetTexture("_LeftTex", skybox1.GetTexture("_LeftTex"));
        camSkybox.SetTexture("_RightTex", skybox1.GetTexture("_RightTex"));
    }

    private void setTexture2(Material skybox2)
    {
        camSkybox.SetTexture("_FrontTex2", skybox2.GetTexture("_FrontTex"));
        camSkybox.SetTexture("_BackTex2", skybox2.GetTexture("_BackTex"));

        camSkybox.SetTexture("_UpTex2", skybox2.GetTexture("_UpTex"));
        camSkybox.SetTexture("_DownTex2", skybox2.GetTexture("_DownTex"));

        camSkybox.SetTexture("_LeftTex2", skybox2.GetTexture("_LeftTex"));
        camSkybox.SetTexture("_RightTex2", skybox2.GetTexture("_RightTex"));
    }
}
