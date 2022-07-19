using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : InteractableBase
{
    [SerializeField] AbilityEnum m_ability;
    public override void OnPlayerEnter()
    {
        AbilityStackHandler.instance.GetAbility(m_ability).gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        base.OnPlayerEnter();
    }
    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }
}
