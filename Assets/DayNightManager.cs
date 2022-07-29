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
    [SerializeField] private GameObject dayClouds;
    [SerializeField] private GameObject nightClouds;

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

    //
    [SerializeField] private bool startState;
    private void Start()
    {
        isDay = startState;
        Refresh();
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
        StartCoroutine(RefreshRoutine());
    }
    private IEnumerator RefreshRoutine()
    {
        float curDur;
        curDur = 0;
        if (doRain)
        {
            rain.SetActive(!isDay);
        }
        if (doClouds)
        {
            dayClouds.SetActive(isDay);
            nightClouds.SetActive(!isDay);
        }
        float duration = 2f;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            
            if (doLight)
            {
                Color startCol = isDay ? nightLight : dayLight;
                Color endCol = isDay ? dayLight : nightLight;
                light.color = Color.Lerp(startCol, endCol, curDur);

                float start = isDay ? nightLightIntensity : dayLightIntensity;
                float end = isDay ? dayLightIntensity : nightLightIntensity;
                light.intensity = Mathf.Lerp(start, end, curDur);
            }
            if (doFog)
            {
                Color startCol = isDay ? nightFog : dayFog;
                Color endCol = isDay ? dayFog : nightFog;
                RenderSettings.fogColor = Color.Lerp(startCol, endCol, curDur);

                float start = isDay ? nightDensity : dayDensity;
                float end = isDay ? dayDensity : nightDensity;
                RenderSettings.fogDensity = Mathf.Lerp(start ,end, curDur);
            }
            if (doSky)
            {
                RenderSettings.skybox = isDay ? daySky : nightSky;
            }
            
            yield return null;
        }
    }
}
