using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueIdleState : State
{
    public bool playerInSight;
    [SerializeField,FoldoutGroup("Properties")] private float sightRange;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {

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
