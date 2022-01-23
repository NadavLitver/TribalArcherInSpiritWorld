using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathMeter : MonoBehaviour
{
    //OOB = out of breath
    [SerializeField, FoldoutGroup("Refrences")]
    private Image m_fill;
    [SerializeField, FoldoutGroup("Refrences")]
    private Slider m_slider;
    [SerializeField, FoldoutGroup("Refrences")]
    private Breath r_breath;
    [SerializeField, FoldoutGroup("Refrences")]
    private GameObject m_buffer;

    [SerializeField, FoldoutGroup("Colors")] private Color IdleColor;
    [SerializeField, FoldoutGroup("Colors")] private Color OutOfBreathColor;
    [SerializeField, FoldoutGroup("Colors")] private Color FlashColor;
    
    [SerializeField, FoldoutGroup("Parameters")] private float lerpSpeed;
    [SerializeField, FoldoutGroup("Parameters")] private float lerpDuration;

    [SerializeField, FoldoutGroup("Flash")] private float flashDuration;
    [SerializeField, FoldoutGroup("Flash")] private AnimationCurve FlashEase;
    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        Color startColor;
        startColor = m_fill.color;
        float currDurr = 0;
        while (currDurr < flashDuration)
        {
            currDurr += Time.deltaTime;
            m_fill.color = Color.Lerp(startColor, FlashColor, FlashEase.Evaluate(currDurr / flashDuration));
            yield return new WaitForEndOfFrame();
        }
        m_fill.color = IdleColor;
    }
    public void EnterOutOfBreath()
    {
        m_buffer.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ChangeColorTo(OutOfBreathColor));
    }
    public void ExitOutOfBreath()
    {
        m_buffer.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(ChangeColorTo(IdleColor));
    }

    private IEnumerator ChangeColorTo(Color targetColor)
    {
        float currDurr = 0;
        Color startColor = m_fill.color;
        while (currDurr < lerpDuration)
        {
            currDurr += Time.deltaTime;
            m_fill.color = Color.Lerp(startColor, targetColor, currDurr / lerpDuration);
            yield return new WaitForEndOfFrame();
        }
        m_fill.color = targetColor;
    }
    private void LateUpdate()
    {
        m_slider.value = r_breath.current / r_breath.maxBreath;
    }
}