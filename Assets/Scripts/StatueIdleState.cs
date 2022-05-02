using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueIdleState : State
{
    public bool playerInSight;
    [SerializeField,FoldoutGroup("Properties")] private float sightRange;
    [SerializeField, FoldoutGroup("Refrences")] GameObject beamLine;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        beamLine.SetActive(false);
    }
    private void Update()
    {

        playerInSight = Physics.Raycast(transform.position, ((PlayerController.playerTransform.position - stateHandler.body.transform.position).normalized), sightRange, playerLayer);
        if (playerInSight)
        {
            SwapToNextState();
        }
    }

}
