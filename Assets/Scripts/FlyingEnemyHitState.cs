using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyHitState : State
{
    public float hitStateLength;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        if (_animators != null)
            foreach (Animator item in _animators)
            {
                item.Play("Hit");
            }
        StartCoroutine(SwapStateDelay());
    }
    IEnumerator SwapStateDelay()
    {
        yield return new WaitForSeconds(hitStateLength);
        agent.isStopped = false;
        SwapToNextState();
    }
}
