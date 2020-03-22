// GENERATED AUTOMATICALLY FROM 'Assets/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""b70388ab-b06e-4576-a665-9691204dc2bb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""75e68d71-e66a-4cdd-83e7-4636410437c6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Button"",
                    ""id"": ""20820594-1a89-447c-81f3-d35b342c841c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonX"",
                    ""type"": ""Button"",
                    ""id"": ""14b87448-0bcc-437d-bf7d-37eff97726ee"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonB"",
                    ""type"": ""Button"",
                    ""id"": ""aa489604-0b82-4ba2-865f-d930c055681e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5cab7af2-b410-4836-85d4-7ad50f0c0659"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bce0818-2d50-4b32-b2e2-0988a3607f6d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca97c933-92ca-4e43-9229-09bf0659b5d7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9420aaca-e3c6-4b6a-9dea-2a304b774534"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_RightStick = m_Gameplay.FindAction("RightStick", throwIfNotFound: true);
        m_Gameplay_ButtonX = m_Gameplay.FindAction("ButtonX", throwIfNotFound: true);
        m_Gameplay_ButtonB = m_Gameplay.FindAction("ButtonB", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_RightStick;
    private readonly InputAction m_Gameplay_ButtonX;
    private readonly InputAction m_Gameplay_ButtonB;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @RightStick => m_Wrapper.m_Gameplay_RightStick;
        public InputAction @ButtonX => m_Wrapper.m_Gameplay_ButtonX;
        public InputAction @ButtonB => m_Wrapper.m_Gameplay_ButtonB;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @RightStick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightStick;
                @RightStick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightStick;
                @RightStick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightStick;
                @ButtonX.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonX;
                @ButtonX.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonX;
                @ButtonX.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonX;
                @ButtonB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonB;
                @ButtonB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonB;
                @ButtonB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonB;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RightStick.started += instance.OnRightStick;
                @RightStick.performed += instance.OnRightStick;
                @RightStick.canceled += instance.OnRightStick;
                @ButtonX.started += instance.OnButtonX;
                @ButtonX.performed += instance.OnButtonX;
                @ButtonX.canceled += instance.OnButtonX;
                @ButtonB.started += instance.OnButtonB;
                @ButtonB.performed += instance.OnButtonB;
                @ButtonB.canceled += instance.OnButtonB;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRightStick(InputAction.CallbackContext context);
        void OnButtonX(InputAction.CallbackContext context);
        void OnButtonB(InputAction.CallbackContext context);
    }
}
