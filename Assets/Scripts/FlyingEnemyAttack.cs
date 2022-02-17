using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : State
{
    public float timeBetweenAttacks;
    public float BackStepLength;
    bool alreadyAttacked;
    public ObjectPool pool;
    public Transform shootPoint;
    public float projectileForce;
    public float faceTargetSpeed = 50;
    protected override void OnStateDisabled()
    {
        alreadyAttacked = false;

    }

    protected override void OnStateEnabled()
    {
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
        var turnTowardNavSteeringTarget = agent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * faceTargetSpeed);
    }
    private void AttackPlayer()
    {
        var dir = PlayerController.playerTransform.position - transform.position;

        var goal = transform.position + (-dir.normalized * BackStepLength);

        //Make sure enemy doesn't move
      //  agent.SetDestination(goal);

       // transform.LookAt(PlayerController.playerTransform);
        if (alreadyAttacked || PlayerController.playerTransform == null)
            return;
        var arrow = pool.GetPooledObject();
        arrow.transform.position = shootPoint.position;
        arrow.transform.rotation = shootPoint.rotation;
        var arrowProj = arrow.GetComponent<ProjectileBase>();
        arrowProj.direction = (PlayerController.playerTransform.position - shootPoint.position).normalized;
        arrowProj.force = projectileForce;
        arrow.SetActive(true);
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
        SwapToNextState();
    }
}
