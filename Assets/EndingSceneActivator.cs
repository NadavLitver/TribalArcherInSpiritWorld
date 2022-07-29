using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneActivator : InteractableBase
{
    [SerializeField] DayNightManager nightManager;
    public override void Interact()
    {
        base.Interact();
        nightManager.Toggle();
    }
}
