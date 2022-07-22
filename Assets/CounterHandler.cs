using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CounterHandler : MonoBehaviour
{
    private int currentCount = 0;
    public bool finishedCounting { get; private set; }
    //refs
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI regularText;
    //params
    [SerializeField] private int maxCount;
    [SerializeField] private Color idleColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private AnimationCurve shineEase;

    [SerializeField] private Image[] parts;
    [SerializeField] private AnimationClip completedAnim;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        finishedCounting = false;
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].color = idleColor;
        }
    }
    public void CountUp(float delay)
    {
        StartCoroutine(countRoutine(delay));
    }
    private IEnumerator countRoutine(float delay)
    {
        if (finishedCounting)
        {
            Debug.Log("already finished");
            StopCoroutine(countRoutine(delay));
        }
        currentCount++;
        numberText.text = currentCount.ToString();
        yield return new WaitForSeconds(delay);
        float curDur = 0;
        float duration = 0.4f;
        Color[] startColors = new Color[parts.Length];
        for (int i = 0; i < startColors.Length; i++)
        {
            startColors[i] = parts[i].color;
        }
        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            if (regularText != null)
            {
                regularText.color = Color.Lerp(idleColor, activeColor, shineEase.Evaluate(curDur));
            }
            if (numberText != null)
            {
                numberText.color = Color.Lerp(idleColor, activeColor, shineEase.Evaluate(curDur));
            }
            for (int i = 0; i < parts.Length; i++)
            {
                if (currentCount > i)
                {
                    parts[i].color = Color.Lerp(startColors[i], activeColor, curDur);
                }
                else
                {
                    parts[i].color = Color.Lerp(startColors[i], idleColor, curDur);
                }
            }
            yield return null;
        }
        if (regularText != null)
        {
            regularText.color = idleColor;
        }
        if (numberText != null)
        {
            numberText.color = idleColor;
        }
        for (int i = 0; i < parts.Length; i++)
        {
            if (currentCount > i)
            {
                parts[i].color = Color.Lerp(startColors[i], activeColor, curDur);
            }
            else
            {
                parts[i].color = Color.Lerp(startColors[i], idleColor, curDur);
            }
        }
        if (currentCount == maxCount)
        {
            finishedCounting = true;
            //needs to play exit / completed anim, maybe even bool to differentiate between exit states dunno
            //GetComponent<Animation>().Play(completedAnim.name);
        }
    }
}
