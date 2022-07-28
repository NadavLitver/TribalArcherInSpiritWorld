
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
        foreach (Animator item in _animators)
        {
            item.SetTrigger("Chase");
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Target.position.x, Target.position.z));
        Debug.Log(distance);
        playerInShootRange = distance < rangeToShoot;
        if (playerInShootRange)
        {
            SwapToNextState();
        }
        ChasePlayer();/*Physics.Raycast(transform.position, ((Target.position - stateHandler.body.transform.position).normalized), rangeToShoot, playerLayer);*/


    }
    private void ChasePlayer()
    {

        if (Target.position != null)
            agent.SetDestination(Target.position);
    }

}
