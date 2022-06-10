using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBoltAOEAbility : Ability
{
   
    public GameObject ps_equipped;
    public ObjectPool LightingArrowPool;
    public Animator m_animator;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityQ.AddListener(ToggleAbility);

    }
    public override void ToggleAbility()
    {
        base.ToggleAbility();
        m_animator.Play("LoadLightingStrike");
        ps_equipped.SetActive(AbilityToggle);
    }
   
}
