using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterArrowAbility : Ability
{
    [SerializeField] GameObject[] uXExtraArrows;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityR.AddListener(ToggleAbility);

    }
    protected override void ToggleAbility()
    {
        base.ToggleAbility();
        for (int i = 0; i < uXExtraArrows.Length; i++)
        {
            uXExtraArrows[i].SetActive(AbilityToggle);
        }
    }
  
}
