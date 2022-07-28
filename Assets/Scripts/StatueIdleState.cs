using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueIdleState : State
{
    public bool playerInSight;
    [SerializeField,FoldoutGroup("Properties")] private float sightRange;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {

    }
    private void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Target.position.x, Target.position.z));
        Debug.Log(distance);
        playerInSight = distance < sightRange;
        if (playerInSight)
        {
            SwapToNextState();
        }
    }

}
