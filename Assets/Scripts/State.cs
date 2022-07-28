using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{

    [FoldoutGroup("Properties")] public LayerMask groundLayer, playerLayer;

    [FoldoutGroup("Properties")] public bool canExitToHit;


    [SerializeField, FoldoutGroup("Refrences")] protected List<Animator> _animators;

    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] protected LivebodyStateHandler stateHandler;

    [SerializeField, FoldoutGroup("Refrences")] protected State nextState;

    [FoldoutGroup("Refrences")] public NavMeshAgent agent;

    [SerializeField, ReadOnly, FoldoutGroup("Refrences")] protected Transform Target;
    [SerializeField, FoldoutGroup("Properties")] protected bool isTempleTarget;
    private void Awake()
    {
        stateHandler = GetComponent<LivebodyStateHandler>();
        

    }

    private void OnEnable()
    {
        Target = isTempleTarget ? TempleBody.TempleTransform : PlayerController.playerTransform;
        OnStateEnabled();

    }
    public void ToggleTarget()
    {
        isTempleTarget = !isTempleTarget;
        Target = isTempleTarget ? TempleBody.TempleTransform : PlayerController.playerTransform;

    }
    protected abstract void OnStateEnabled();
    private void OnDisable()
    {
        StopAllCoroutines();
        OnStateDisabled();
        if (_animators != null)
            foreach (Animator item in _animators)
            {
                item.speed = 1;
            }
    }
    protected abstract void OnStateDisabled();

    protected virtual void SwapToNextState()
    {
        stateHandler.SwapState(nextState);
    }
  
}
