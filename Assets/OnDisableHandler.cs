using UnityEngine;
using UnityEngine.Events;

public class OnDisableHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDisableEvent;
    private void OnEnable()
    {
        if (OnDisableEvent == null)
        {
            OnDisableEvent = new UnityEvent();
        }
    }

    private void OnDisable()
    {
        OnDisableEvent.Invoke();
    }
}
