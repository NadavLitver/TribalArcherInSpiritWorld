using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public bool isDay { get; private set; }

    //rain
    [SerializeField] private bool doRain = true;
    [SerializeField] private GameObject rain;

    //clouds
    [SerializeField] private bool doClouds = true;
    [SerializeField] private GameObject clouds;

    //light
    [SerializeField] private bool doLight = true;
    [SerializeField] private Light light;
    [SerializeField] private Color dayLight;
    [SerializeField] private Color nightLight;
    [SerializeField] private float dayLightIntensity;
    [SerializeField] private float nightLightIntensity;

    //fog
    [SerializeField] private bool doFog = true;
    [SerializeField] private Color dayFog;
    [SerializeField] private Color nightFog;
    [SerializeField] private float dayDensity = 0.001f;
    [SerializeField] private float nightDensity = 0.007f;

    //sky
    [SerializeField] private bool doSky = true;
    [SerializeField] private Material daySky;
    [SerializeField] private Material nightSky;


    private void Start()
    {
        SetDay();
    }
    [Button]
    public void Toggle()
    {
        isDay = !isDay;
        Refresh();
    }
    public void SetDay()
    {
        isDay = true;
        Refresh();
    }
    public void SetNight()
    {
        isDay = false;
        Refresh();
    }
    private void Refresh()
    {
        if (doRain)
        {
            rain.SetActive(!isDay);
        }
        if (doClouds)
        {
            clouds.SetActive(!isDay);
        }
        if (doLight)
        {
            light.color = isDay ? dayLight : nightLight;
            light.intensity = isDay ? dayLightIntensity : nightLightIntensity;
        }
        if (doFog)
        {
            RenderSettings.fogColor = isDay ? dayFog : nightFog;
            RenderSettings.fogDensity = isDay ? dayDensity : nightDensity;
        }
        if (doSky)
        {
            RenderSettings.skybox = isDay ? daySky : nightSky;
        }
    }
}
