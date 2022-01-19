using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class InputManager : MonoBehaviour
{
    private PlayerInputs inputActions;
    private static InputManager _instace;
    public UnityEvent OnPlayerStartShoot;
    public UnityEvent OnPlayerReleaseShoot;
    public UnityEvent OnPlayerFinishCharge;


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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputActions.PlayerMap.Shoot.started += PlayerStartedShootThisFrame;
        inputActions.PlayerMap.Shoot.performed += PlayerFinishCharging; 
        inputActions.PlayerMap.Shoot.canceled += PlayerReleaseShootThisFrame;


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

    public bool IsHoldingSprint()
    {
        Debug.Log(inputActions.PlayerMap.Sprint.ReadValue<float>());
        bool pressingSprintKey = inputActions.PlayerMap.Sprint.ReadValue<float>() > 0;
        bool movingForward = inputActions.PlayerMap.Movement.ReadValue<Vector2>().y > 0;
        return pressingSprintKey && movingForward;
    }
  


}
