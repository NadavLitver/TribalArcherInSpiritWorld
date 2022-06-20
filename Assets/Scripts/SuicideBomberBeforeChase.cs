using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberBeforeChase : State
{
    private float timeBeforeChase =0.5f;
    private float faceTargetSpeed = 90;

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
    //private void Update()
    //{
    //    //FaceTarget();
    //}
    IEnumerator SwapStateDelay()
    {
        yield return new WaitForSeconds(timeBeforeChase);
        agent.isStopped = false;
        _animator.SetTrigger("Chase");
        SwapToNextState();
    }
    void FaceTarget()
    {
        var turnTowardNavSteeringTarget = PlayerController.playerTransform.position;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceTargetSpeed);
    }
}
