using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemySideMove : State
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float Range;
    private Vector3 destination;
    private Vector3 direction;
    private float timeInState;
    protected override void OnStateDisabled()
    {
        agent.isStopped = false;

    }

    protected override void OnStateEnabled()
    {
        timeInState = 0;
        agent.isStopped = true;
        agent.ResetPath();
        direction = RandomBoolean() ? transform.right : -transform.right;
        //agent.isStopped = false;
        destination = transform.position + (direction * Range);

    }

    // Update is called once per frame
    void Update()
    {
        timeInState += Time.deltaTime;
        agent.Move((direction + transform.forward)* Time.deltaTime * speed);
        FaceTarget();
        if ((transform.position - destination).magnitude < 2f || timeInState > 1.5f)
        {
            SwapToNextState();
            
        }
    }
    void FaceTarget()
    {
        var turnTowardNavSteeringTarget = PlayerController.playerTransform.position;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        stateHandler.body.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(destination, 1);
    }
    private bool RandomBoolean()
    {
        return (Random.value > 0.5f);
    }
}
