using Sirenix.OdinInspector;
using UnityEngine;

public class FlyingChaseState : State
{
    public float sightRange;
    public bool playerInSight;
    [ReadOnly,SerializeField]
    private float timeInState;
    public int timeInSecondsToSideJump;
    public State SideStep;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = false;
        timeInState = 0;
        

    }

    private void Update()
    {
        timeInState += Time.deltaTime;
        ChasePlayer();
        playerInSight = Physics.Raycast(transform.position, ((PlayerController.playerTransform.position - stateHandler.body.transform.position).normalized), sightRange, playerLayer);
        if (playerInSight)
        {
            SwapToNextState();
        }
        else
        {
            if(timeInState > timeInSecondsToSideJump)
            {
                stateHandler.SwapState(SideStep);
            }
        }

    }
    private void ChasePlayer()
    {

        if (PlayerController.playerTransform != null)
            agent.SetDestination(PlayerController.playerTransform.position);
    }

}
