using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool AbilityToggle;
    public int stackCost;
    public virtual void ToggleAbility()
    {
        if (!AbilityToggle && AbilityStackHandler.instance.currentStackAmount < stackCost)
            return;
        AbilityToggle = !AbilityToggle;
    }
}
