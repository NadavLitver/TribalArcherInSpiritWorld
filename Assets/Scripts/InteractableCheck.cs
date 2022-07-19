using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableCheck : MonoBehaviour
{
    public UnityEvent OnInteractClicked;
    [ReadOnly, SerializeField]
    private bool isNearInteractable;
    [ReadOnly, SerializeField]
    private List<GameObject> interactablesNearMe;
    public PlayerController PlayerRef;
    
    private void Awake()
    {
        if (OnInteractClicked == null)
            OnInteractClicked = new UnityEvent();
        interactablesNearMe = new List<GameObject>();
        if (PlayerRef == null)
            PlayerRef = GetComponentInParent<PlayerController>();
    }
    void OnEnable()
    {
        SetInput();
    }
    public void SetInput()
    {
        InputManager.Instance.OnPlayerStartInteract.AddListener(OnInteract);

    }

    private void OnInteract()
    {
        OnInteractClicked?.Invoke();
        ExecuteInteract();

    }

    private void ExecuteInteract()
    {
        GameObject GOToInteract = GetObjectToInteract();
        if (GOToInteract != null)
            GOToInteract.GetComponent<InteractableBase>()?.Interact();
    }

    private GameObject GetObjectToInteract()
    {
        float distance = 0;
        GameObject GOToInteract = null;
        for (int i = 0; i < interactablesNearMe.Count; i++)
        {
            if (interactablesNearMe[i] == null)
                continue;
            float currentDistance = (interactablesNearMe[i].transform.position - transform.position).magnitude;
            if (currentDistance > distance)
            {
                distance = currentDistance;
                GOToInteract = interactablesNearMe[i];

            }

        }

        return GOToInteract;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Interactor collided with " + collision.name);
        interactablesNearMe.Add(collision.gameObject);
        isNearInteractable = true;
        var Ibase = collision.GetComponent<InteractableBase>() ?? collision.GetComponentInChildren<InteractableBase>();
        if (Ibase != null)
        {
            Ibase.IsCloseToPlayer = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        var Ibase = collision.GetComponent<InteractableBase>();
        if (Ibase != null)
        {
            Ibase.IsCloseToPlayer = false;

        }
        interactablesNearMe.Remove(collision.gameObject);
        if (interactablesNearMe.Count == 0)
            isNearInteractable = false;


    }
    
    void OnDisable()
    {
        InputManager.Instance.OnPlayerStartInteract.RemoveListener(OnInteract);
    }
}
