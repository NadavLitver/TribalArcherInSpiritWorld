using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ability : MonoBehaviour
{
    public bool AbilityToggle;
    public int stackCost;
    public UnityEvent onAbilityToggle;
    public virtual void ToggleAbility()
    {
        if (!AbilityToggle && AbilityStackHandler.instance.currentStackAmount < stackCost)
            return;
        AbilityToggle = !AbilityToggle;
        onAbilityToggle?.Invoke();
    }
}
