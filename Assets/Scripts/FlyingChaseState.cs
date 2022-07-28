using Sirenix.OdinInspector;
using UnityEngine;

public class FlyingChaseState : State
{
    [FoldoutGroup("Refrences")] public State SideStep;

    [SerializeField, FoldoutGroup("Properties")] private float sightRange;
    [SerializeField, FoldoutGroup("Properties")] private bool TargetInSight;
    [ReadOnly, FoldoutGroup("Properties")] public float timeInState;
    [FoldoutGroup("Properties")] public int timeInSecondsToSideJump;
    [SerializeField, FoldoutGroup("Refrences")] private FloatyObject floatComponent;
    [SerializeField, FoldoutGroup("Refrences")] FlyingEnemyGfxHandler gfx;
    [SerializeField, FoldoutGroup("Properties")] bool changeSpeed;
    [SerializeField, FoldoutGroup("Properties")] float newSpeed;
    private float prevSpeed;
   

    protected override void OnStateDisabled()
    {
        if (floatComponent != null && !floatComponent.enabled)
        {
            floatComponent.enabled = false;
        }
        if(changeSpeed)
        {
            agent.speed = prevSpeed;
        }
    }

    protected override void OnStateEnabled()
    {
        agent.isStopped = false;
        timeInState = 0;
        if (floatComponent != null && floatComponent.enabled)
            floatComponent.enabled = false;
        if (gfx != null)
            gfx.ResetGFXRotation();
        if (changeSpeed)
        {
            prevSpeed = agent.speed;
            agent.speed = newSpeed;
            SoundManager.Play(SoundManager.Sound.SuicideCruising, stateHandler.body.audioSource,0.4f);
         
        }
    }
   
    private void Update()
    {
        timeInState += Time.deltaTime;
        ChaseTarget();
        // TargetInSight = Physics.Raycast(transform.position, ((Target.position - stateHandler.body.transform.position).normalized), sightRange, playerLayer);
        TargetInSight = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Target.position.x, Target.position.z)) < sightRange;
        if (TargetInSight)
        {
            SwapToNextState();
        }
        else
        {
            if (timeInState > timeInSecondsToSideJump &&!isTempleTarget)
            {
                stateHandler.SwapState(SideStep);
            }
        }

    }
    private void ChaseTarget()
    {

        if (Target != null)
            agent.SetDestination(Target.position);
    }

}
