using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingChaseState : State
{
    public float sightRange;
    public bool playerInSight;
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
        ChasePlayer();
    }
    private void ChasePlayer()
    {
       if(PlayerController.playerTransform != null)
        agent.SetDestination(PlayerController.playerTransform.position);
    }

}
