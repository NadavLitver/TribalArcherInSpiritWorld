using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof (CanvasGroup))]
public class GroupHandler : MonoBehaviour
{
    CanvasGroup m_group;
    [SerializeField] private bool startState = true;
    private bool isActive = false;
    private void Start()
    {
        isActive = startState;
        m_group = GetComponent<CanvasGroup>();
        if (startState)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }
    public void Enable()
    {
        StartCoroutine(FadeToCoru(true));
    }
    public void Disable()
    {
        StartCoroutine(FadeToCoru(false));
    }

    public void Toggle()
    {
        isActive = !isActive;
        StartCoroutine(FadeToCoru(isActive));
    }
    private IEnumerator FadeToCoru(bool Active)
    {
        m_group.blocksRaycasts = Active;
        m_group.interactable = Active;
        float currDurr = 0f;
        float duration = 0.25f;
        float target = Active ? 1 : 0;
        while (currDurr < duration)
        {
            currDurr += Time.deltaTime;
            m_group.alpha = Mathf.Lerp(m_group.alpha, target, currDurr / duration);
            yield return new WaitForEndOfFrame();
        }
    }

}
