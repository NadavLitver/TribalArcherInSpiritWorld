using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : State
{
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public ObjectPool pool;
    public Transform shootPoint;
    public float projectileForce;
    protected override void OnStateDisabled()
    {
        alreadyAttacked = false;

    }

    protected override void OnStateEnabled()
    {
        StartCoroutine(AttackDelay());
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(PlayerController.playerTransform);
        if (alreadyAttacked || PlayerController.playerTransform == null)
            return;
        var arrow = pool.GetPooledObject();
        arrow.transform.position = shootPoint.position;
        arrow.transform.rotation = shootPoint.rotation;
        var arrowProj = arrow.GetComponent<Projectile>();
        arrowProj.direction = (PlayerController.playerTransform.position - shootPoint.position).normalized;
        arrowProj.force = projectileForce;
        arrowProj.isRelease = true;
        arrow.SetActive(true);
        alreadyAttacked = true;
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
        SwapToNextState();
    }
}
