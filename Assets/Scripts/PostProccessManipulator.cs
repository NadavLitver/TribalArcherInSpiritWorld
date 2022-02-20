using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProccessManipulator : MonoBehaviour
{
    private static Volume volume;
    private static LensDistortion lensDistortion;
    [SerializeField, FoldoutGroup("Properties")]
    float lensDistortionOnShoot;
    [SerializeField, FoldoutGroup("Properties")]
    float lensDistortionOnSprint;
    private static float distortionValueOnSprint;
    private static float distortionValueOnShoot;

    void Start()
    {
        volume = GetComponent<Volume>();
        distortionValueOnSprint = lensDistortionOnSprint;
        distortionValueOnShoot = lensDistortionOnShoot;
    }
    public static void SetLensDistortion()
    {
        if (distortionValueOnSprint == 0)
            return;
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
    public static void LensDistortionOnShoot()
    {
        if (distortionValueOnShoot == 0)
            return;
        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            volume.StartCoroutine(LensDistortionOnShoot(lensDistortion));
        }
    }
    private static IEnumerator LensDistortionRoutine(LensDistortion lens)
    {
       
        float curDur = 0;
        float currentIntenstity = lens.intensity.value;
        while (lens.intensity.value != distortionValueOnSprint)
        {
            lens.intensity.value = Mathf.Lerp(currentIntenstity, distortionValueOnSprint, curDur / 1);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

    }
    private static IEnumerator LensDistortionOnShoot(LensDistortion lens)
    {
        float curDur = 0.5f;
        float currentIntenstity = lens.intensity.value;
        while (lens.intensity.value != distortionValueOnShoot)
        {
            lens.intensity.value = Mathf.Lerp(currentIntenstity, distortionValueOnShoot, curDur / 1);
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
            lens.intensity.value = Mathf.Lerp(currentIntenstity, 0, curDur / 2f);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        curDur = 0;
        while(lens.scale.value != 1f)
        {
            lens.scale.value = Mathf.Lerp(currentScale, 1f, curDur / 2f);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }
}

