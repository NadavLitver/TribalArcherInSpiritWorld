using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : InteractableBase
{
    [SerializeField] AbilityEnum m_ability;
    [SerializeField] private Image icon;
    [SerializeField] private Image iconCover;
    [SerializeField] private AnimationCurve coverEase;
    [SerializeField] private AnimationCurve iconEase;

    [SerializeField] private Color idleColor;
    [SerializeField] private Color activeColor;

    [SerializeField] private float idleEmission;
    [SerializeField] private float activeEmission;
    [SerializeField] private AnimationCurve emissionEase;
    [SerializeField] private List<MeshRenderer> m_renderers;
    [SerializeField] private List<ParticleSystem> auraEffects;
    public override void OnPlayerEnter()
    {
        Glow();
    }
    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }
    private void Update()
    {
        foreach (MeshRenderer item in m_renderers)
        {
            item.material.SetFloat("_EmissionPower", idleEmission + Mathf.Sin(Time.time * 3f));
        }
    }
    public void Glow()
    {
        StartCoroutine(GlowRoutine());
    }

    private IEnumerator GlowRoutine()
    {
        foreach (ParticleSystem item in auraEffects)
        {
            ParticleSystem.EmissionModule em;
            em = item.emission;
            em.rateOverTime = 0f;
        }

        base.OnPlayerEnter();
        float curDur;
        curDur = 0;
        float duration = 1f;
        string emissionRef = "_EmissionPower";

        icon.color = idleColor;
        iconCover.color = idleColor;
        foreach (MeshRenderer item in m_renderers)
        {
            item.material.SetFloat(emissionRef, idleEmission);
        }

        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            foreach (MeshRenderer item in m_renderers)
            {
                item.material.SetFloat(emissionRef, Mathf.Lerp(idleEmission, activeEmission, emissionEase.Evaluate(curDur)));
            }
            yield return null;
        }
        foreach (MeshRenderer item in m_renderers)
        {
            item.material.SetFloat(emissionRef, activeEmission);
        }
        duration = 2;
        curDur = 0;
        yield return new WaitForSeconds(1);
        AbilityStackHandler.instance.GetAbility(m_ability).gameObject.SetActive(true);
        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            iconCover.color = Color.Lerp(idleColor, activeColor, coverEase.Evaluate(curDur));
            icon.color = Color.Lerp(idleColor, activeColor, iconEase.Evaluate(curDur));
        }
        icon.color = idleColor;
        iconCover.color = activeColor;
        this.gameObject.SetActive(false);
    }
}
