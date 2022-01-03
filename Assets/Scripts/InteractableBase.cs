using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBase : MonoBehaviour
{
    
    private bool _isCloseToPlayer;
    public bool IsCloseToPlayer
    {
        get { return _isCloseToPlayer; }
        set
        {
            if (value == _isCloseToPlayer)
                return;

            _isCloseToPlayer = value;
            if (_isCloseToPlayer)
                OnPlayerEnter();
            else
                OnPlayerExit();


        }
    }
    public UnityEvent OnInteract;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public virtual void Interact()
    {
        OnInteract?.Invoke();
    }
    public virtual void OnPlayerEnter()
    {
        OnEnter?.Invoke();
    }
    public virtual void OnPlayerExit()
    {
        OnExit?.Invoke();
    }

}
