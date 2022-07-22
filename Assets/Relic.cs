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
    [SerializeField] private CanvasGroup letterGroup;

    [SerializeField] private Color idleColor;
    [SerializeField] private Color activeColor;

    [SerializeField] private Color idleIntensity;
    [SerializeField] private Color activeIntensity;

    [SerializeField] private float idleEmission;
    [SerializeField] private float activeEmission;
    [SerializeField] private AnimationCurve emissionEase;
    [SerializeField] private List<MeshRenderer> m_renderers;
    [SerializeField] private List<ParticleSystem> auraEffects;
    [SerializeField] private ParticleSystem consumeEffect;
    private bool isConsumed = false;

    private void Start()
    {
        consumeEffect.gameObject.SetActive(false);
        letterGroup.alpha = 0;
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
        TextBox.instance.Activate("Scatter shot added", 3f);
        base.OnPlayerEnter();
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

        icon.color = idleIntensity;
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
            item.enabled = false;
        }
        duration = 0.5f;
        curDur = 0;
        yield return new WaitForSeconds(2.9f);
        AbilityStackHandler.instance.GetAbility(m_ability).gameObject.SetActive(true);
        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            letterGroup.alpha = Mathf.Lerp(0, 1, curDur);
            iconCover.color = Color.Lerp(idleColor, activeColor, coverEase.Evaluate(curDur));
            icon.color = Color.Lerp(idleIntensity, activeIntensity, iconEase.Evaluate(curDur));
            Debug.Log(curDur);
            yield return null;
        }
        icon.color = idleIntensity;
        iconCover.color = activeColor;
        this.gameObject.SetActive(false);
    }
}
