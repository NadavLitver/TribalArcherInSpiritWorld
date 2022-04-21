using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueEnemyChase : State
{

    public float rangeToShoot;
    public bool playerInShootRange;
  
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = false;


    }

    private void Update()
    {
        ChasePlayer();
        playerInShootRange = Physics.Raycast(transform.position, ((PlayerController.playerTransform.position - stateHandler.body.transform.position).normalized), rangeToShoot, playerLayer);
        if (playerInShootRange)
        {
            SwapToNextState();
        }


    }
    private void ChasePlayer()
    {

        if (PlayerController.playerTransform != null)
            agent.SetDestination(PlayerController.playerTransform.position);
    }

}
