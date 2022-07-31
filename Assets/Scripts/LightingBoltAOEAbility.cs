using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBoltAOEAbility : Ability
{
   
    public GameObject ps_equipped;
    public ObjectPool LightingArrowPool;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityQ.AddListener(ToggleAbility);

    }
    public override void ToggleAbility()
    {
        Debug.Log(this.name + "ability toggle:" + AbilityToggle);

        base.ToggleAbility(); 
        ps_equipped.SetActive(AbilityToggle);
    }
   
}
