using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterArrowAbility : Ability
{
    [SerializeField] Animator m_animator;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityR.AddListener(ToggleAbility);

    }
    public override void ToggleAbility()
    {
        base.ToggleAbility();
        if (AbilityToggle)
        {
            m_animator.Play("ScatterLoad");

        }
        else
        {
            m_animator.Play("Load");

        }
    }
  
}
