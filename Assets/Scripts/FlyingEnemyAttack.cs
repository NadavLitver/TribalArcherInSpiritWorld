using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : State
{
    public float timeBetweenAttacks;
    public float BackStepLength;
    bool alreadyAttacked;
    public ObjectPool pool;
    public Transform rightHand;
    public Transform leftHand;
    public float projectileForce;
    public float faceTargetSpeed = 50;
    protected override void OnStateDisabled()
    {
       

    }

    protected override void OnStateEnabled()
    {
        alreadyAttacked = false;
        agent.isStopped = true;
        //agent.updatePosition = false;
       
        StartCoroutine(AttackDelay());

    }
    private void Update()
    {
        FaceTarget();
    }
    void FaceTarget()
    {
        var turnTowardNavSteeringTarget = PlayerController.playerTransform.position;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceTargetSpeed);
    }
    private void AttackPlayer()
    {
      
        if (alreadyAttacked || PlayerController.playerTransform == null)
            return;
       
        var Shot = pool.GetPooledObject();
        var shootPos = RandomBoolean() ? rightHand : leftHand;
        Shot.transform.SetPositionAndRotation(shootPos.position, shootPos.rotation);
        _animator.SetTrigger("Throw");
        var ProjBase = Shot.GetComponent<ProjectileBase>();
        ProjBase.direction = (PlayerController.playerTransform.position - shootPos.position).normalized;
        ProjBase.force = projectileForce;
        Shot.SetActive(true);
        alreadyAttacked = true;
        SoundManager.Play(SoundManager.Sound.OwlAttack, stateHandler.body.audioSource);
        StartCoroutine(SwapStateDelay());

    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(timeBetweenAttacks * 0.5f);
        AttackPlayer();
    }
    IEnumerator SwapStateDelay()
    {

        yield return new WaitForSeconds(timeBetweenAttacks * 0.5f);
        agent.SetDestination(PlayerController.playerTransform.position);
        agent.isStopped = false;
        SwapToNextState();
    }
    private bool RandomBoolean()
    {
        return (Random.value > 0.55f);
    }
}
