using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class LivebodyStateHandler : MonoBehaviour
{
    [FoldoutGroup("Refrences"), ReadOnly]
    public Livebody body;
   
    [SerializeField, Tooltip("Insert all states of current livebody, call the SwapState function from each state to set the current state"), FoldoutGroup("Properties")]
    private List<State> states;
    [SerializeField, Tooltip("Specifically the state the livebody goes to when hit"), FoldoutGroup("Properties")]
    public State hitState;
    [SerializeField, Tooltip("Specifically the state the livebody goes to when hit"), FoldoutGroup("Properties")]
    public State stunState;
    private void Awake()
    {
        body = GetComponentInParent<Livebody>();
        if (body != null)
            body.m_stateHandler = this;
    }
    private void Start()
    {
        if(EnemySpawnerManager.instance != null)
        EnemySpawnerManager.instance.AddMe(body);

    }
    public void OnEnable()
    {
        body.health = body.maxHealth;
        body.isVulnerable = true;
        InitStates();
        if(AbilityStackHandler.instance != null)
          AbilityStackHandler.instance.playerBody.OnDeath.AddListener(InitStates);
       

    }
    public void OnDisable()
    {
        if (AbilityStackHandler.instance != null)
            AbilityStackHandler.instance.playerBody.OnDeath.RemoveListener(InitStates);
    }
    public void InitStates()
    {
        SwapState(states[0]);

    }
    public State GetCurrentState()
    {
        foreach (State _state in states)
        {
            if(_state.enabled == true)
            {
                return _state;
            }
        }
        return states[0];
    }
    public void SwapState(State state)
    {
        foreach (State _state in states)
        {
            _state.enabled = false;
            if (_state == state)
                _state.enabled = true;

            else
                _state.enabled = false;

        }
    }
    [Button, FoldoutGroup("Buttons")]
    void SwapStateInspectorButton(State state)
    {
        SwapState(state);
    }
    //[Button, FoldoutGroup("Buttons")]
    public void SwapToHitState()
    {
        foreach (State _state in states)
        {
            if(_state.enabled && _state.canExitToHit)
                  SwapState(hitState);

        }

    }
    [Button]
    public void ToggleAllStatesTarget()
    {
        foreach (State _state in states)
        {
            _state.ToggleTarget();
        }
    }
    public void SwapToStunState()
    {
        if(stunState!= null)
        SwapState(stunState);

    }
}
