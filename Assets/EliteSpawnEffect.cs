using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EliteSpawnEffect : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] m_renderers;
    [SerializeField] private SkinnedMeshRenderer[] m_tempRenderers;
    [SerializeField] private AnimationCurve lerpEase;
    [SerializeField] private AnimationCurve scaleEase;

    [SerializeField] private Transform body;
    [SerializeField] private UnityEvent endEvent;

    [SerializeField] private Material mat;

    [SerializeField] private float targetScale = 4;
    private void OnEnable()
    {
        if (endEvent == null)
        {
            endEvent = new UnityEvent();
        }
        StartCoroutine(ActiveRoutine());
    }
    private IEnumerator ActiveRoutine()
    {
        float startSize = body.localScale.x;
        float endSize = targetScale;
        float curDur = 0f;
        float duration = 2f;

        float start = 10;
        float end = 0;
        Material[] tempMats;
        tempMats = new Material[2];
        tempMats[0] = m_renderers[0].material;
        tempMats[1] = mat;

        foreach (SkinnedMeshRenderer item in m_renderers)
        {
            item.materials = tempMats;
        }
        foreach (SkinnedMeshRenderer item in m_tempRenderers)
        {
            item.materials = tempMats;
        }

        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            body.localScale = Vector3.one * Mathf.Lerp(startSize, endSize, scaleEase.Evaluate(curDur));
            foreach (SkinnedMeshRenderer renderer in m_renderers)
            {
                renderer.materials[1].SetFloat("_DissolveAmount", Mathf.Lerp(start, end, lerpEase.Evaluate(curDur)));
            }
            foreach (SkinnedMeshRenderer renderer in m_tempRenderers)
            {
                renderer.materials[1].SetFloat("_DissolveAmount", Mathf.Lerp(start, end, lerpEase.Evaluate(curDur)));
            }
            yield return null;
        }
        endEvent.Invoke();
        curDur = 0;
        duration = 1f;
        start = 0;
        end = 1.5f;

        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            foreach (SkinnedMeshRenderer renderer in m_renderers)
            {
                renderer.materials[1].SetFloat("_DissolveAmount", Mathf.Lerp(start, end, lerpEase.Evaluate(curDur)));
            }
            foreach (SkinnedMeshRenderer renderer in m_tempRenderers)
            {
                renderer.materials[1].SetFloat("_DissolveAmount", Mathf.Lerp(start, 10, lerpEase.Evaluate(curDur)));
            }
            yield return null;
        }
        tempMats[1] = null;
        foreach (SkinnedMeshRenderer renderer in m_renderers)
        {
            renderer.materials[1].SetFloat("_DissolveAmount", end);
        }
        foreach (SkinnedMeshRenderer item in m_tempRenderers)
        {
            item.materials = tempMats;
        }
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
