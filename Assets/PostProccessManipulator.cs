using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProccessManipulator : MonoBehaviour
{
    private static Volume volume;
    private static LensDistortion lensDistortion;

    void Start()
    {
        volume = GetComponent<Volume>();

    }
    public static void SetLensDistortion()
    {

        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            volume.StartCoroutine(LensDistortionRoutine(lensDistortion));
        }
    }
    public static void ResetLensDistortion()
    {

        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            volume.StartCoroutine(LensDistortionResetRoutine(lensDistortion));
        }
    }
    public static void LensDistortionScale()
    {

        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            volume.StartCoroutine(LensDistortionScale(lensDistortion));
        }
    }
    private static IEnumerator LensDistortionRoutine(LensDistortion lens)
    {
        float curDur = 0;
        float currentIntenstity = lens.intensity.value;
        while (lens.intensity.value != -0.6f)
        {
            lens.intensity.value = Mathf.Lerp(currentIntenstity, -0.6f, curDur/1);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

    }
    private static IEnumerator LensDistortionScale(LensDistortion lens)
    {
        float curDur = 0.5f;
        float currentIntenstity = lens.intensity.value;
        float currentScale = lens.scale.value;
        while (lens.intensity.value != -0.1f &&  lens.scale.value != 1)
        {
            lens.intensity.value = -0.1f;
            lens.scale.value = Mathf.Lerp(currentScale, 1.15f, curDur / 1);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

    }
    private static IEnumerator LensDistortionResetRoutine(LensDistortion lens)
    {
        float curDur = 0;
        float currentIntenstity = lens.intensity.value;
        float currentScale = lens.scale.value;
        while (lens.intensity.value != 0 )
        {
            lens.intensity.value = Mathf.Lerp(currentIntenstity, 0, curDur / 1f);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        curDur = 0;
        while(lens.scale.value != 1f)
        {
            lens.scale.value = Mathf.Lerp(currentScale, 1f, curDur / 1f);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }
}

