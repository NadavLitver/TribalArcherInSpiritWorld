using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectHandler : MonoBehaviour
{
    [SerializeField] private float effectDuration = 0.25f;
    [SerializeField] private Color idleColor = Color.black;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private float maxIntensity = 5f;
    [SerializeField] private AnimationCurve m_ease;

    [SerializeField] private List<SkinnedMeshRenderer> m_skinnedMeshRenderers;

    private const string colorRef = "_FresnelColor";
    private void OnEnable()
    {
        foreach (SkinnedMeshRenderer item in m_skinnedMeshRenderers)
        {
            item.material.SetColor(colorRef, idleColor);
        }
    }
    public void Hit()
    {
        StopCoroutine(this.HitRoutine());
        VFXManager.instance.StartCoroutine(HitRoutine());
    }
    private IEnumerator HitRoutine()
    {
        float curDur;
        curDur = 0;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / effectDuration;
            float ease = m_ease.Evaluate(curDur);
            Debug.Log(curDur);
            foreach (SkinnedMeshRenderer item in m_skinnedMeshRenderers)
            {
                for (int i = 0; i < item.materials.Length; i++)
                {
                    item.materials[i].SetColor(colorRef, Color.Lerp(idleColor, activeColor, ease));
                }
            }
            yield return null;
        }
        foreach (SkinnedMeshRenderer item in m_skinnedMeshRenderers)
        {

            item.materials[0].SetColor(colorRef, idleColor);
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
