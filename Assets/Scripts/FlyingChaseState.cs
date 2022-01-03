using UnityEngine;

public class FlyingChaseState : State
{
    public float sightRange;
    public bool playerInSight;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = false;

    }

    private void Update()
    {
        ChasePlayer();
        playerInSight = Physics.Raycast(transform.position, ((PlayerController.playerTransform.position - stateHandler.body.transform.position).normalized), sightRange, playerLayer);
        if (playerInSight)
        {
            SwapToNextState();
        }

    }
    private void ChasePlayer()
    {

        if (PlayerController.playerTransform != null)
            agent.SetDestination(PlayerController.playerTransform.position);
    }

}
