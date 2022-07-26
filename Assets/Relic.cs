using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : InteractableBase
{
    [SerializeField] AbilityEnum m_ability;

    [SerializeField] private float idleEmission;
    [SerializeField] private float activeEmission;
    [SerializeField] private AnimationCurve emissionEase;
    [SerializeField] private List<MeshRenderer> m_renderers;
    [SerializeField] private List<ParticleSystem> auraEffects;
    [SerializeField] private ParticleSystem consumeEffect;

    [SerializeField] private Animation anim;
    private bool isConsumed = false;

    private void Start()
    {
        consumeEffect.gameObject.SetActive(false);
    }
    public override void OnPlayerEnter()
    {
        if (!isConsumed)
        {
            isConsumed = true;
            Glow();
        }
    }
    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }
    public void Glow()
    {
        StartCoroutine(GlowRoutine());
    }

    private IEnumerator GlowRoutine()
    {
        base.OnPlayerEnter();
        anim.Play();
        consumeEffect.gameObject.SetActive(true);
        consumeEffect.transform.parent = null;
        foreach (ParticleSystem item in auraEffects)
        {
            ParticleSystem.EmissionModule em;
            em = item.emission;
            em.rateOverTime = 0f;
        }

        float curDur;
        curDur = 0;
        float duration = 1f;
        string emissionRef = "_EmissionPower";

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
            item.enabled = false;
        }
        duration = 0.5f;
        curDur = 0;
        yield return new WaitForSeconds(2.9f);
        string text;
        switch (m_ability)
        {
            case AbilityEnum.Scatter: text = "Scattershot Acquired";
                break;
            case AbilityEnum.QuickShot: text = "Lightningbolt Acquired";
                break;
            case AbilityEnum.LightingStrike: text = "Lighting Strike Acquired";
                break;
            default: text = "ha";
                break;
        }
        TextBox.instance.Activate(text, 0f);
        AbilityStackHandler.instance.GetAbility(m_ability).gameObject.SetActive(true);
        
        this.gameObject.SetActive(false);
    }
}
