using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFlash : MonoBehaviour
{
    [SerializeField] private Image target;
    [SerializeField] private Slider sliderRef;
    private Image image;

    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private AnimationCurve ease;
    [SerializeField] private float flashDuration = 0.5f;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        float curDur = 0;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / flashDuration;
            image.color = Color.Lerp(startColor, targetColor, ease.Evaluate(curDur));
            image.fillAmount = target.fillAmount;
            yield return null;
        }
    }
}
