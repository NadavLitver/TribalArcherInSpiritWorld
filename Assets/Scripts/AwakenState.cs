using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenState : State
{
    [SerializeField, FoldoutGroup("Properties")] private float timeToNextState;
    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {
        foreach (Animator item in _animators)
        {
            item.Play("Awaken");
        }
        StartCoroutine(delayNextStateSwap());
    }
    IEnumerator delayNextStateSwap()
    {
        yield return new WaitForSeconds(timeToNextState);
        SwapToNextState();
    }
    // Start is called before the first frame update
  
}
