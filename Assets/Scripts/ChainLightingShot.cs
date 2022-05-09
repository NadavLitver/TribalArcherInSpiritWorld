using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChainLightingShot : Ability
{
    public GameObject ArrowToSpin;
    public ObjectPool ChainLightingArrowPool;
    public UnityEvent OnQuickShotToggle;
    //private Quaternion arrowToSpinStartingRotation;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityF.AddListener(ToggleAbility);
    }
    protected override void ToggleAbility()
    {
        base.ToggleAbility();
    }

    public void ResetArrowToSpin()
    {
        ArrowToSpin.transform.localEulerAngles = Quaternion.Euler(Vector3.zero).eulerAngles;
    }
    private void Update()
    {
        if (AbilityToggle)
        {
            
            ArrowToSpin.transform.RotateAround(ArrowToSpin.transform.position, transform.forward, Time.deltaTime * 180f);
        }

    }
}
