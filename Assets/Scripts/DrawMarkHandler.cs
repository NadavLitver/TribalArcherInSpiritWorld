using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class DrawMarkHandler : MonoBehaviour
{
    [FoldoutGroup("Refrences"), SerializeField]
    private CrosshairCornerHandler[] CrosshairCorners;
    [FoldoutGroup("Refrences"), SerializeField]
    private BowHandler playerBow;
    [FoldoutGroup("Properties"), SerializeField, ReadOnly]
    private bool isPlayerCharging;
    [FoldoutGroup("Properties"), SerializeField]
    private float lerpSpeed = 150;
    [FoldoutGroup("Properties"), SerializeField]
    private CanvasGroup m_canvasGroup;

    private bool doSine;
    private float startTime;
    private float startScale = 1f;
    private float endScale = 0.7f;

    private float startRot = 0f;
    private float maxRot = 15f;

    [SerializeField] private AnimationCurve rotEase;
    [SerializeField] private AnimationCurve scaleEase;

    private void Start()
    {
        doSine = false;
        m_canvasGroup.alpha = 0;
    }
    private void Update()
    {

        if (doSine)
        {
            transform.localScale = Vector3.one * (endScale + (Mathf.Sin((Time.time - startTime)) * Time.deltaTime * 1f));
        }
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
        float duration = 1f;
        float currDurr = 0;
        transform.rotation = Quaternion.identity;
        while (currDurr < duration)
        {
            currDurr +=  Time.deltaTime;
            Debug.Log(currDurr);
            transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, scaleEase.Evaluate(currDurr));
            m_canvasGroup.alpha = Mathf.Lerp(0, 1, currDurr / duration);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startRot, maxRot, rotEase.Evaluate(currDurr)));
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.one * endScale;
        m_canvasGroup.alpha = 1;
        doSine = true;
        startTime = Time.time;
    }
    public void OnPlayerReleaseCharge()
    {
        StopAllCoroutines();
        StartCoroutine(OnPlayerReleaseChargeRoutine());
    }
    private IEnumerator OnPlayerReleaseChargeRoutine()
    {
        Debug.Log("release");
        isPlayerCharging = false;
        doSine = false;
        float currDurr = 0f;
        float duration = 0.2f;
        SetCornersBackToStartingPos();
        transform.localScale = startScale * Vector3.one;
        while (currDurr < duration)
        {
            currDurr += Time.deltaTime;
            m_canvasGroup.alpha = Mathf.Lerp(1, 0, currDurr / duration);
            transform.localScale = Vector3.one * Mathf.Lerp(endScale, 1.5f, currDurr / duration);

            yield return new WaitForEndOfFrame();
        }
        m_canvasGroup.alpha = 0;
        transform.localScale = Vector3.one * 1.5f;
        //Debug.Log("release end");
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
                CrosshairCorners[i].transform.localPosition = Vector3.MoveTowards(CrosshairCorners[i].transform.localPosition, CrosshairCorners[i].goal, playerBow.shootHoldTime * 500 * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public void ReleaseArrow()
    {

    }
}
