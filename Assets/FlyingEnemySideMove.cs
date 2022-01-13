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
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = true;
        agent.ResetPath();
        direction = RandomBoolean() ? transform.right : -transform.right;
        //agent.isStopped = false;
        destination = transform.position + (direction * Range);

    }

    // Update is called once per frame
    void Update()
    {

        agent.Move(direction * Time.deltaTime * speed);
        if((transform.position - destination).magnitude < 2f)
        {
            SwapToNextState();
            
        }
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
