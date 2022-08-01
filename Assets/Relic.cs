using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : InteractableBase
{
    [SerializeField] AbilityEnum m_ability;

    //[SerializeField] private float idleEmission;
    //[SerializeField] private float activeEmission;
    //[SerializeField] private AnimationCurve emissionEase;
    //[SerializeField] private List<MeshRenderer> m_renderers;
    //[SerializeField] private List<ParticleSystem> auraEffects;
    //[SerializeField] private ParticleSystem consumeEffect;
    [SerializeField] private Animation anim;
    private bool isConsumed = false;

    private void Start()
    {
        //consumeEffect.gameObject.SetActive(false);
    }
    public override void Interact()
    {
        
        if (!isConsumed)
        {
            base.Interact();
            isConsumed = true;
            SoundManager.Play(SoundManager.Sound.RelicPickup, transform.position);
            GetAbility();
        }
    }
    public override void OnPlayerEnter()
    {
       
    }
    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }
    public void GetAbility()
    {
        StartCoroutine(GetAbilityRoutine());
    }

    private IEnumerator GetAbilityRoutine()
    {
        anim.Play();
        yield return new WaitForSeconds(1.5f);
        string text = m_ability switch
        {
            AbilityEnum.Scatter => "Scattershot Acquired",
            AbilityEnum.QuickShot => "Lightningbolt Acquired",
            AbilityEnum.LightingStrike => "Lighting Strike Acquired",
            _ => "ha",
        };
        TextBox.instance.Activate(text, 2f);
        AbilityStackHandler.instance.GetAbility(m_ability).gameObject.SetActive(true);

    
    }
}
