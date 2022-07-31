using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterArrowAbility : Ability
{
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityR.AddListener(ToggleAbility);

    }
    public override void ToggleAbility()
    {
        base.ToggleAbility();
    }
  
}
