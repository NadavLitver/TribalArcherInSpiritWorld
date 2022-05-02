using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProccessManipulator : MonoBehaviour
{
    private static Volume volume;
    private static LensDistortion lensDistortion;
    private static Vignette vignette;
    [SerializeField, FoldoutGroup("Properties")]
    float lensDistortionOnShoot;
    [SerializeField, FoldoutGroup("Properties")]
    float lensDistortionOnSprint;
    [SerializeField, FoldoutGroup("Properties")]
    float maxVignette;
    [SerializeField, FoldoutGroup("Properties")]
    float VignetteOnHitAddition;
    private static float DistortionValueOnSprint;
    private static float DistortionValueOnShoot;
    private static float MaximumVignetteVal;
    private static float VignetteOnHitAdditionValue;

    void Start()
    {
        volume = GetComponent<Volume>();
        DistortionValueOnSprint = lensDistortionOnSprint;
        DistortionValueOnShoot = lensDistortionOnShoot;
        MaximumVignetteVal = maxVignette;
        VignetteOnHitAdditionValue = VignetteOnHitAddition;
    }
    public static void SetLensDistortion()
    {
        if (DistortionValueOnSprint == 0)
            return;
        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            volume.StartCoroutine(LensDistortionRoutine(lensDistortion));
        }
    }
    public static void SetVignette(float currentHP)
    {
       
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            if (MaximumVignetteVal == 0)
                return;
           float newVignetteVal = (((100 -currentHP) * MaximumVignetteVal) / 100);
            Debug.Log("new VIGNETTE VALUE IS " + newVignetteVal);
            volume.StartCoroutine(VignetteRoutine(vignette, newVignetteVal));
        }
    }
    public static void OnHitVignette()
    {

        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            volume.StartCoroutine(OnHitVignetteRoutine(vignette));
        }
        else
        {
            Debug.LogError("Vignette not found");
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
        if (DistortionValueOnShoot == 0)
            return;
        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            volume.StartCoroutine(LensDistortionOnShoot(lensDistortion));
        }
        else
        {
            Debug.LogError("Vignette not found");
        }
    }
    private static IEnumerator LensDistortionRoutine(LensDistortion lens)
    {
       
        float curDur = 0;
        float currentIntenstity = lens.intensity.value;
        while (lens.intensity.value != DistortionValueOnSprint)
        {
            lens.intensity.value = Mathf.Lerp(currentIntenstity, DistortionValueOnSprint, curDur / 1);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

    }
    private static IEnumerator VignetteRoutine(Vignette vignette,float value)
    {
        float curDur = 0;
        float currentIntenstity = vignette.intensity.value;
        while (vignette.intensity.value != value)
        {
            vignette.intensity.value = Mathf.Lerp(currentIntenstity, value, curDur / 1);
            curDur += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        
    }
    private static IEnumerator OnHitVignetteRoutine(Vignette vignette)
    {
        float duration = 0.2f;

        float curDur = 0;
        float currentIntenstity = vignette.intensity.value;
        while (curDur < 1)
        {
            vignette.intensity.value = Mathf.Lerp(currentIntenstity, currentIntenstity + VignetteOnHitAdditionValue, curDur);
            curDur += Time.deltaTime / (duration / 2);
            yield return new WaitForEndOfFrame();
        }
        curDur = 1;
        while (curDur > 0)
        {
            vignette.intensity.value = Mathf.Lerp(currentIntenstity, currentIntenstity + VignetteOnHitAdditionValue, curDur);
            curDur -= Time.deltaTime / (duration / 2);
            yield return new WaitForEndOfFrame();
        }
        vignette.intensity.value = currentIntenstity;
    }
    private static IEnumerator LensDistortionOnShoot(LensDistortion lens)
    {
        float curDur = 0.5f;
        float currentIntenstity = lens.intensity.value;
        while (lens.intensity.value != DistortionValueOnShoot)
        {
            lens.intensity.value = Mathf.Lerp(currentIntenstity, DistortionValueOnShoot, curDur / 1);
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
    void OnDisable()
    {
        volume = null;
       
    }
}

