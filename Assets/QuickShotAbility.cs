using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuickShotAbility : MonoBehaviour
{
    public bool AbilityToggle;
    public GameObject ArrowToSpin;
    public UnityEvent OnQuickShotToggle;
    private Quaternion arrowToSpinStartingRotation;
    private void Awake()
    {
        AbilityToggle = false;
        InputManager.Instance.OnPlayerClickAbilityF.AddListener(ToggleAbility);
    }
    private void ToggleAbility()
    {

        //if (AbilityToggle)
        //{
        //    ResetArrowToSpin();
        //    AbilityToggle = false;
        //}
        //else
        //{
        //    AbilityToggle = true;
        //}
        AbilityToggle = !AbilityToggle;
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
