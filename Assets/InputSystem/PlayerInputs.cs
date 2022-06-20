// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""PlayerMap"",
            ""id"": ""5f5303f0-9a90-44fa-8568-9f383d9f3ff3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""442fc7a8-7632-4e69-9724-a3a78664c5ac"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c97cdd54-321b-46fa-a272-bd51d0f5f170"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f27bcd44-252f-4a11-8bfc-5818f956c1ea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4ed68eed-1bc8-43f5-b6a6-bfd59142cf7d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.68)""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8d7cd792-ae79-4cb8-bd28-7b76a128b452"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityF"",
                    ""type"": ""Button"",
                    ""id"": ""d52bd369-f4f9-42e4-bfae-83d37b7ccbb6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityR"",
                    ""type"": ""Button"",
                    ""id"": ""9f5cf5ad-1e43-4812-81c5-a59917f1e24c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AbilityQ"",
                    ""type"": ""Button"",
                    ""id"": ""5fe41219-00d1-4093-b727-7cce4db0335f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""734d6a00-54d7-41bb-8c55-3adae9fd1bca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sens"",
                    ""type"": ""Value"",
                    ""id"": ""7ca5079a-095a-47f3-bf2a-e2094ccebbee"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""0c594f4f-7a2a-4fd7-acec-0552c64f2583"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c2013fc8-0c81-411f-bc7b-9325c7343f82"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""faf3fd7d-f9b2-4f12-b93b-b5276c702629"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4a7034e6-bda7-4f84-9833-5495efd80b7c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c6d0e5f-ec8f-4874-bb1a-b80a48b3beb9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2433c407-e886-4faf-9eaf-5209d5b6f159"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8d8999fd-3337-4179-b55d-b618ccaf1251"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e679199a-6467-4eb1-a52e-8eefaa5028e4"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec98dd03-7ca9-4557-b838-30477c6b0629"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1649e7b7-1f78-4917-bc45-84b2a10aee95"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b5de100-0a1c-4220-95a6-7da3444c025d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityF"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c3bc088-caf6-40c1-b56b-130a44621c4d"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c6ab09eb-365a-4364-aeb6-ec3565443446"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sens"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9ade65b5-c3d2-45e0-9322-ea334ce53222"",
                    ""path"": ""<Keyboard>/pageDown"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sens"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e5087ee2-869e-4049-ba08-7e365f31b962"",
                    ""path"": ""<Keyboard>/pageUp"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sens"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4a12a55c-595f-4314-a8bb-db3944a12b74"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4884178a-e983-4d50-af85-5326f6d691eb"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AbilityQ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eee5a4b-0995-40db-9189-763cdc70b482"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GeneralMap"",
            ""id"": ""62c07b62-1225-4895-bc5a-35eedf49aa6a"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""f208d728-2797-4dab-9ae1-6a6a62c571a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""491290ae-dfc5-4960-ab61-fe7d6da4d556"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMap
        m_PlayerMap = asset.FindActionMap("PlayerMap", throwIfNotFound: true);
        m_PlayerMap_Movement = m_PlayerMap.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMap_Jump = m_PlayerMap.FindAction("Jump", throwIfNotFound: true);
        m_PlayerMap_Look = m_PlayerMap.FindAction("Look", throwIfNotFound: true);
        m_PlayerMap_Shoot = m_PlayerMap.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerMap_Interact = m_PlayerMap.FindAction("Interact", throwIfNotFound: true);
        m_PlayerMap_AbilityF = m_PlayerMap.FindAction("AbilityF", throwIfNotFound: true);
        m_PlayerMap_AbilityR = m_PlayerMap.FindAction("AbilityR", throwIfNotFound: true);
        m_PlayerMap_AbilityQ = m_PlayerMap.FindAction("AbilityQ", throwIfNotFound: true);
        m_PlayerMap_Sprint = m_PlayerMap.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerMap_Sens = m_PlayerMap.FindAction("Sens", throwIfNotFound: true);
        m_PlayerMap_Exit = m_PlayerMap.FindAction("Exit", throwIfNotFound: true);
        // GeneralMap
        m_GeneralMap = asset.FindActionMap("GeneralMap", throwIfNotFound: true);
        m_GeneralMap_Pause = m_GeneralMap.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerMap
    private readonly InputActionMap m_PlayerMap;
    private IPlayerMapActions m_PlayerMapActionsCallbackInterface;
    private readonly InputAction m_PlayerMap_Movement;
    private readonly InputAction m_PlayerMap_Jump;
    private readonly InputAction m_PlayerMap_Look;
    private readonly InputAction m_PlayerMap_Shoot;
    private readonly InputAction m_PlayerMap_Interact;
    private readonly InputAction m_PlayerMap_AbilityF;
    private readonly InputAction m_PlayerMap_AbilityR;
    private readonly InputAction m_PlayerMap_AbilityQ;
    private readonly InputAction m_PlayerMap_Sprint;
    private readonly InputAction m_PlayerMap_Sens;
    private readonly InputAction m_PlayerMap_Exit;
    public struct PlayerMapActions
    {
        private @PlayerInputs m_Wrapper;
        public PlayerMapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMap_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerMap_Jump;
        public InputAction @Look => m_Wrapper.m_PlayerMap_Look;
        public InputAction @Shoot => m_Wrapper.m_PlayerMap_Shoot;
        public InputAction @Interact => m_Wrapper.m_PlayerMap_Interact;
        public InputAction @AbilityF => m_Wrapper.m_PlayerMap_AbilityF;
        public InputAction @AbilityR => m_Wrapper.m_PlayerMap_AbilityR;
        public InputAction @AbilityQ => m_Wrapper.m_PlayerMap_AbilityQ;
        public InputAction @Sprint => m_Wrapper.m_PlayerMap_Sprint;
        public InputAction @Sens => m_Wrapper.m_PlayerMap_Sens;
        public InputAction @Exit => m_Wrapper.m_PlayerMap_Exit;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMapActions instance)
        {
            if (m_Wrapper.m_PlayerMapActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnLook;
                @Shoot.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnShoot;
                @Interact.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnInteract;
                @AbilityF.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityF;
                @AbilityF.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityF;
                @AbilityF.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityF;
                @AbilityR.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityR;
                @AbilityR.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityR;
                @AbilityR.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityR;
                @AbilityQ.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityQ;
                @AbilityQ.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityQ;
                @AbilityQ.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnAbilityQ;
                @Sprint.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSprint;
                @Sens.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSens;
                @Sens.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSens;
                @Sens.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnSens;
                @Exit.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnExit;
            }
            m_Wrapper.m_PlayerMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @AbilityF.started += instance.OnAbilityF;
                @AbilityF.performed += instance.OnAbilityF;
                @AbilityF.canceled += instance.OnAbilityF;
                @AbilityR.started += instance.OnAbilityR;
                @AbilityR.performed += instance.OnAbilityR;
                @AbilityR.canceled += instance.OnAbilityR;
                @AbilityQ.started += instance.OnAbilityQ;
                @AbilityQ.performed += instance.OnAbilityQ;
                @AbilityQ.canceled += instance.OnAbilityQ;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Sens.started += instance.OnSens;
                @Sens.performed += instance.OnSens;
                @Sens.canceled += instance.OnSens;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
            }
        }
    }
    public PlayerMapActions @PlayerMap => new PlayerMapActions(this);

    // GeneralMap
    private readonly InputActionMap m_GeneralMap;
    private IGeneralMapActions m_GeneralMapActionsCallbackInterface;
    private readonly InputAction m_GeneralMap_Pause;
    public struct GeneralMapActions
    {
        private @PlayerInputs m_Wrapper;
        public GeneralMapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_GeneralMap_Pause;
        public InputActionMap Get() { return m_Wrapper.m_GeneralMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralMapActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralMapActions instance)
        {
            if (m_Wrapper.m_GeneralMapActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_GeneralMapActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GeneralMapActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GeneralMapActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_GeneralMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public GeneralMapActions @GeneralMap => new GeneralMapActions(this);
    public interface IPlayerMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAbilityF(InputAction.CallbackContext context);
        void OnAbilityR(InputAction.CallbackContext context);
        void OnAbilityQ(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnSens(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
    }
    public interface IGeneralMapActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
