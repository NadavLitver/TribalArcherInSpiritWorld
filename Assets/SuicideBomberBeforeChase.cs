using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberBeforeChase : State
{
    private float timeBeforeChase;

    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = true;
        _animator.SetTrigger("BeforeChase");
        SoundManager.Play(SoundManager.Sound.SuicideDetect, stateHandler.body.audioSource);
        StartCoroutine(SwapStateDelay());
    }

    IEnumerator SwapStateDelay()
    {
        yield return new WaitForSeconds(timeBeforeChase);
        agent.isStopped = false;
        _animator.SetTrigger("Chase");
        SwapToNextState();
    }
}
