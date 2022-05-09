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
        StartCoroutine(SwapStateDelay());
        if (_animator != null)
            _animator.Play("Hit");
    }
    IEnumerator SwapStateDelay()
    {
        yield return new WaitForSeconds(hitStateLength);
        SwapToNextState();
    }
}
