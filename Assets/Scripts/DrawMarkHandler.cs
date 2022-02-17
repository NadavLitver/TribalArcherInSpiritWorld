using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawMarkHandler : MonoBehaviour
{
    [FoldoutGroup("Refrences"),SerializeField]
    private CrosshairCornerHandler[] CrosshairCorners;
    [FoldoutGroup("Refrences"), SerializeField]
    private BowHandler playerBow;
    [FoldoutGroup("Properties"), SerializeField,ReadOnly]
    private bool isPlayerCharging;
    [FoldoutGroup("Properties"), SerializeField]
    private float lerpSpeed = 150;
    [FoldoutGroup("Properties"), SerializeField]
    private CanvasGroup m_canvasGroup;

    private float startScale;

    private void Start()
    {
        startScale = transform.localScale.x;
    }

    public void OnPlayerStartCharge()
    {
        isPlayerCharging = true;
        StopAllCoroutines();
        StartCoroutine(OnPlayerStartChargeRoutine());
        StartCoroutine(LerpCrosshairCornersRoutine());
    }
    private IEnumerator OnPlayerStartChargeRoutine()
    {
        float duration = 0.15f;
        float currDurr = 0;
        while (currDurr < duration)
        {
            currDurr += Time.deltaTime;
            transform.localScale = Vector3.one * Mathf.Lerp(0.45f, startScale, currDurr / duration);
            m_canvasGroup.alpha = Mathf.Lerp(0, 1, currDurr / duration);
            yield return new WaitForEndOfFrame();
        }
    }
    public void OnPlayerReleaseCharge()
    {
        StartCoroutine(OnPlayerReleaseChargeRoutine());
    }
    private IEnumerator OnPlayerReleaseChargeRoutine()
    {
        Debug.Log("release");
        isPlayerCharging = false;
        float currDurr = 0f;
        float duration = 0.2f;
        SetCornersBackToStartingPos();
        transform.localScale = startScale * Vector3.one;
        while (currDurr < duration)
        {
            currDurr += Time.deltaTime;
            m_canvasGroup.alpha = Mathf.Lerp(1, 0, currDurr / duration);
            transform.localScale = Vector3.one * Mathf.Lerp(startScale, 1.75f, currDurr / duration);

            yield return new WaitForEndOfFrame();
        }
        Debug.Log("release end");
    }
    void SetCornersBackToStartingPos()
    {
        for (int i = 0; i < CrosshairCorners.Length; i++)
        {
            CrosshairCorners[i].transform.localPosition = CrosshairCorners[i].startingPos;
        }
    }
    IEnumerator LerpCrosshairCornersRoutine()
    {
        while (isPlayerCharging)
        {
            for (int i = 0; i < CrosshairCorners.Length; i++)
            {
                CrosshairCorners[i].transform.localPosition = Vector3.MoveTowards(CrosshairCorners[i].transform.localPosition, CrosshairCorners[i].goal, playerBow.shootHoldTime * 500 *  Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void ReleaseArrow()
    {

    }
}
