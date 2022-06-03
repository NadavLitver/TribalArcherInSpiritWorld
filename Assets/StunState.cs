using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class StunState : State
{
    [FoldoutGroup("Proprties"), SerializeField] private float stunDuration;
    [FoldoutGroup("Proprties"), SerializeField] private GameObject stunEffect;

    protected override void OnStateDisabled()
    {
    }

    protected override void OnStateEnabled()
    {

        PauseStateCall(stunDuration);
    }
    public void PauseStateCall(float duration)
    {
        StartCoroutine(PauseState(duration));
    }
    protected virtual IEnumerator PauseState(float duration)
    {
        stunEffect.SetActive(true);
        if (_animator == null)
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(duration);
            agent.isStopped = false;
            stunEffect.SetActive(false);
            SwapToNextState();
            yield break;
        }

        float prevSpeed = _animator.speed;
        _animator.speed = 0;
        agent.isStopped = true;
        yield return new WaitForSeconds(duration);
        agent.isStopped = false;
        _animator.Play("Hit");
        _animator.speed = prevSpeed;
        stunEffect.SetActive(false);

        SwapToNextState();

    }
}
