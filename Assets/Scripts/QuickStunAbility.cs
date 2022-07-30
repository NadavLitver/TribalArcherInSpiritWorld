using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickStunAbility : Ability
{
    public GameObject ps_equipped;
    public ObjectPool ChainLightingArrowPool;
    public UnityEvent OnQuickShotToggle;
    //private Quaternion arrowToSpinStartingRotation;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityF.AddListener(ToggleAbility);
    }
    public override void ToggleAbility()
    {
        base.ToggleAbility();
        ps_equipped.SetActive(AbilityToggle);
    }

    //public void ResetArrowToSpin()
    //{
    //    ArrowToSpin.transform.localEulerAngles = Quaternion.Euler(Vector3.zero).eulerAngles;
    //}
    //private void Update()
    //{
    //    if (AbilityToggle)
    //    {
            
    //        ArrowToSpin.transform.RotateAround(ArrowToSpin.transform.position, transform.forward, Time.deltaTime * 180f);
    //    }

    //}
}
