using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneActivator : InteractableBase
{
    [SerializeField] DayNightManager nightManager;
    [SerializeField] GameObject PhaseOneFather;
    [SerializeField] GameObject PhaseTwoFather;
    [SerializeField] GameObject PhaseThreeFather;

    bool firstInteract;
    bool secondInteract;
    bool thirdInteract;

    private void Start()
    {
        firstInteract = false;
        secondInteract = false;
        thirdInteract = false;
    }
    public override void Interact()
    {
        if (!firstInteract)//first phase
        {
            base.Interact();
            nightManager.Toggle();
            EnemySpawnerManager.instance.spawn = false;
            firstInteract = true;
            PhaseOneFather.SetActive(true);
            return;
        }
        if (!secondInteract)
        {
            PhaseTwoFather.SetActive(true);

            secondInteract = true;
            return;

        }
        if (!thirdInteract)
        {
            PhaseThreeFather.SetActive(true);

            thirdInteract = true;
            return;

        }

    }
}
