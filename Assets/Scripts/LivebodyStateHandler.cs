using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class LivebodyStateHandler : MonoBehaviour
{
    [FoldoutGroup("Refrences"), ReadOnly]
    public Livebody body;
    [SerializeField, Tooltip("Insert all states of current livebody, call the SwapState function from each state to set the current state"), FoldoutGroup("Properties")]
    private List<State> states;
    [SerializeField, Tooltip("Specifically the state the livebody goes to when hit"), FoldoutGroup("Properties")]
    public State hitState;
    private void Awake()
    {
        body = GetComponentInParent<Livebody>();

    }

    public void Start()
    {
        SwapState(states[0]);
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
}
