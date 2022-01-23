using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-10)]
public class InputManager : MonoBehaviour
{
    private PlayerInputs inputActions;
    private static InputManager _instace;
    public UnityEvent OnPlayerStartShoot;
    public UnityEvent OnPlayerReleaseShoot;
    public UnityEvent OnPlayerFinishCharge;
    public UnityEvent OnPlayerStartInteract;
    public UnityEvent OnPlayerClickAbilityF;
    public UnityEvent OnPlayerStartedSprint;
    public UnityEvent OnPlayerCanceledSprint;

    public UnityEvent OnPause;

    public static InputManager Instance
    {
        get
        {
            return _instace;
        }
    }
    private void Awake()
    {
        inputActions = new PlayerInputs();
        if (_instace != null && _instace != this)
            Destroy(this.gameObject);
        else
            _instace = this;
       
    }
    private void OnEnable()
    {
        inputActions.Enable();
       
        inputActions.PlayerMap.Shoot.started += PlayerStartedShootThisFrame;
        inputActions.PlayerMap.Shoot.performed += PlayerFinishCharging; 
        inputActions.PlayerMap.Shoot.canceled += PlayerReleaseShootThisFrame;
        inputActions.PlayerMap.Interact.started += PlayerStartedInteractThisFrame;
        inputActions.PlayerMap.AbilityF.started += PlayerStartedAbilityEThisFrame;
        inputActions.PlayerMap.Sprint.started += PlayerStartedSprint;
        inputActions.PlayerMap.Sprint.canceled += PlayerCanceledSprint;

        inputActions.GeneralMap.Pause.started += StartPause;
    }

    private void StartPause(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
    }

    private void PlayerCanceledSprint(InputAction.CallbackContext obj)
    {
        OnPlayerCanceledSprint?.Invoke();
    }

    private void PlayerStartedSprint(InputAction.CallbackContext obj)
    {
        OnPlayerStartedSprint?.Invoke();
    }

    private void PlayerFinishCharging(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerFinishCharge?.Invoke();
    }

    private void PlayerReleaseShootThisFrame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerReleaseShoot?.Invoke();
    }

    private void PlayerStartedShootThisFrame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerStartShoot?.Invoke();
    }
    private void PlayerStartedInteractThisFrame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerStartInteract?.Invoke();
    }
    private void PlayerStartedAbilityEThisFrame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerClickAbilityF?.Invoke();
    }

    



    private void OnDisable()
    {
        inputActions.Disable();
    }
    public Vector2 GetPlayerMovement()
    {
        return inputActions.PlayerMap.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return inputActions.PlayerMap.Look.ReadValue<Vector2>();
    }
    public bool PlayerJumpedThisFrame()
    {
        return inputActions.PlayerMap.Jump.triggered;
    }
}
