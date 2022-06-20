using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public UnityEvent OnPlayerClickAbilityR;
    public UnityEvent OnPlayerClickAbilityQ;
    public UnityEvent OnPlayerStartedSprint;
    public UnityEvent OnPlayerCanceledSprint;
    public float shootHoldTime;

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
        string interactionInfo = inputActions.PlayerMap.Shoot.interactions;
        StringBuilder holdTime = new StringBuilder("",3);
        foreach (char c in interactionInfo)
        {
            if(c == '.')
            {
                holdTime.Append(c);
                continue;
            }
            if (char.IsNumber(c))
            {
                holdTime.Append(c);
                continue;
            }
        }
        shootHoldTime = float.Parse(holdTime.ToString());
        inputActions.PlayerMap.Shoot.started += PlayerStartedShootThisFrame;
        inputActions.PlayerMap.Shoot.performed += PlayerFinishCharging; 
        inputActions.PlayerMap.Shoot.canceled += PlayerReleaseShootThisFrame;
        inputActions.PlayerMap.Interact.started += PlayerStartedInteractThisFrame;
        inputActions.PlayerMap.AbilityF.started += PlayerStartedAbilityFThisFrame;
        inputActions.PlayerMap.Sprint.started += PlayerStartedSprint;
        inputActions.PlayerMap.Sprint.canceled += PlayerCanceledSprint;
        inputActions.PlayerMap.AbilityR.started += PlayerStartedAbilityRThisFrame;
        inputActions.PlayerMap.AbilityQ.started += PlayerStartedAbilityQThisFrame;
        inputActions.PlayerMap.Exit.performed += OnExit;
        inputActions.GeneralMap.Pause.started += Pause;
    }

    private void OnExit(InputAction.CallbackContext obj)
    {
        SceneMaster.instance.QuitApp();
    }

    private void PlayerStartedAbilityQThisFrame(InputAction.CallbackContext obj)
    {
        OnPlayerClickAbilityQ?.Invoke();

    }

    private void PlayerStartedAbilityRThisFrame(InputAction.CallbackContext obj)
    {
        OnPlayerClickAbilityR?.Invoke();
    }

    private void Pause(InputAction.CallbackContext obj)
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
    private void PlayerStartedAbilityFThisFrame(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
    public float GetPgUpPgDwnAxis()
    {
        return inputActions.PlayerMap.Sens.ReadValue<float>();
    }
    public bool PlayerJumpedThisFrame()
    {
        return inputActions.PlayerMap.Jump.triggered;
    }
}
