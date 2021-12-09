using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static float slowDownFactor = 0.05f;
    private static float slowDownLength = 1f;

    private void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);
    }
    public static void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
