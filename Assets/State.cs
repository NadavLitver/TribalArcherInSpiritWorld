using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Refrences")]
    protected Animator _animator;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly]
    protected LivebodyStateHandler stateHandler;
    [SerializeField, FoldoutGroup("Refrences")]
    protected State nextState;
    [FoldoutGroup("Properties")]
    public bool canExitToHit;
    [FoldoutGroup("Properties")]
    public LayerMask groundLayer, playerLayer;
    [FoldoutGroup("Refrences")]
    public NavMeshAgent agent;
    private void Awake()
    {
        stateHandler = GetComponent<LivebodyStateHandler>();
        if (stateHandler == null)
            Debug.LogError("No State_Handler Found");

    }
   
    private void OnEnable()
    {
        OnStateEnabled();

    }
    protected abstract void OnStateEnabled();
    private void OnDisable()
    {
        StopAllCoroutines();
        OnStateDisabled();
    }
    protected abstract void OnStateDisabled();

    protected virtual void SwapToNextState()
    {
        stateHandler.SwapState(nextState);
    }
}
