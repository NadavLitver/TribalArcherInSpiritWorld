using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingPatrolState : State
{
    [ReadOnly]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float sightRange;
    public bool playerInSight;
    public bool GroundInSight;
    protected override void OnStateDisabled()
    {

    }

    protected override void OnStateEnabled()
    {
        // agent.updateRotation = true;
        agent.isStopped = false;
        if(_animator != null)
          _animator.Play("Idle"); 
        

    }
    private void Update()
    {

        GroundInSight = Physics.Raycast(transform.position, ((PlayerController.playerTransform.position - stateHandler.body.transform.position).normalized), sightRange, groundLayer);
        if (GroundInSight)
        {
            playerInSight = false;
            Patroling();
            return;
        }
        playerInSight = Physics.Raycast(transform.position, ((PlayerController.playerTransform.position - stateHandler.body.transform.position).normalized), sightRange, playerLayer);
        if (playerInSight)
        {
            SwapToNextState();
        }
        else
        {
            Patroling();
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) RandomPoint(transform.position, walkPointRange, out walkPoint);

        if (walkPointSet && agent.isActiveAndEnabled)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        //    void SearchWalkPoint()
        //    {
        //        //Calculate random point in range
        //        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        //        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        //        {
        //            Vector3 result;
        //            if(RandomPoint(walkPoint,range,out result))
        //            {

        //            }
        //            walkPointSet = true;
        //        }
        //    }
        //}
        //public float range = 10.0f;

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    walkPoint = result;
                    walkPointSet = true;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }

    }
}
