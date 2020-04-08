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
                    ""type"": ""PassThrough"",
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
                },
                {
                    ""name"": ""buttonLT"",
                    ""type"": ""Button"",
                    ""id"": ""49cd6182-e6a1-4b08-a934-d5923b5c9618"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonA"",
                    ""type"": ""Button"",
                    ""id"": ""df8880d5-45f7-4864-bd60-6e8cc09b2617"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonY"",
                    ""type"": ""Button"",
                    ""id"": ""94fb2ed2-400d-499e-9560-b22f78bbd7a0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonStart"",
                    ""type"": ""Button"",
                    ""id"": ""14bbf43c-e962-4137-b254-03d80eb2e355"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""buttonRT"",
                    ""type"": ""Button"",
                    ""id"": ""407e6b20-99ad-407a-975c-e02ca6f571d5"",
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
                    ""id"": ""19966479-34d6-45c4-951d-a8a78614181a"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat"",
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
                    ""id"": ""dbb8d643-981e-4cbf-84d5-9add16dcc836"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button3"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""d0c28b59-5a8d-40b0-8fe0-6ff2b7f88440"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9abe0e8c-7ee9-4dc0-bba9-3ff70f3ae6b2"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonLT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3d5ee84-32d2-4e18-b1ff-bf594cd011a1"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonLT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d6e5de1-44bd-4be4-a6b9-848c9b50a258"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6edc1b8-5f56-4b3c-b3c1-b47b21f5be60"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8310f69e-9a4e-4779-8aa2-2bd8bf3b75d2"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ad07dfc-cd38-4628-8f4c-af4daa0e36ba"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67cc59d9-c363-4a72-abd0-d2af782549f7"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d15bfba6-8804-4787-bcc2-c4e8ea836877"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7499697f-8ba3-4860-a74e-c47c688e2569"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonRT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bb393af-f097-4601-8d5d-8c1b74f90139"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""buttonRT"",
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
        m_Gameplay_buttonLT = m_Gameplay.FindAction("buttonLT", throwIfNotFound: true);
        m_Gameplay_ButtonA = m_Gameplay.FindAction("ButtonA", throwIfNotFound: true);
        m_Gameplay_ButtonY = m_Gameplay.FindAction("ButtonY", throwIfNotFound: true);
        m_Gameplay_ButtonStart = m_Gameplay.FindAction("ButtonStart", throwIfNotFound: true);
        m_Gameplay_buttonRT = m_Gameplay.FindAction("buttonRT", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_buttonLT;
    private readonly InputAction m_Gameplay_ButtonA;
    private readonly InputAction m_Gameplay_ButtonY;
    private readonly InputAction m_Gameplay_ButtonStart;
    private readonly InputAction m_Gameplay_buttonRT;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @RightStick => m_Wrapper.m_Gameplay_RightStick;
        public InputAction @ButtonX => m_Wrapper.m_Gameplay_ButtonX;
        public InputAction @ButtonB => m_Wrapper.m_Gameplay_ButtonB;
        public InputAction @buttonLT => m_Wrapper.m_Gameplay_buttonLT;
        public InputAction @ButtonA => m_Wrapper.m_Gameplay_ButtonA;
        public InputAction @ButtonY => m_Wrapper.m_Gameplay_ButtonY;
        public InputAction @ButtonStart => m_Wrapper.m_Gameplay_ButtonStart;
        public InputAction @buttonRT => m_Wrapper.m_Gameplay_buttonRT;
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
                @buttonLT.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonLT;
                @buttonLT.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonLT;
                @buttonLT.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonLT;
                @ButtonA.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonA;
                @ButtonA.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonA;
                @ButtonA.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonA;
                @ButtonY.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonY;
                @ButtonY.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonY;
                @ButtonY.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonY;
                @ButtonStart.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonStart;
                @ButtonStart.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonStart;
                @ButtonStart.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonStart;
                @buttonRT.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonRT;
                @buttonRT.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonRT;
                @buttonRT.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnButtonRT;
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
                @buttonLT.started += instance.OnButtonLT;
                @buttonLT.performed += instance.OnButtonLT;
                @buttonLT.canceled += instance.OnButtonLT;
                @ButtonA.started += instance.OnButtonA;
                @ButtonA.performed += instance.OnButtonA;
                @ButtonA.canceled += instance.OnButtonA;
                @ButtonY.started += instance.OnButtonY;
                @ButtonY.performed += instance.OnButtonY;
                @ButtonY.canceled += instance.OnButtonY;
                @ButtonStart.started += instance.OnButtonStart;
                @ButtonStart.performed += instance.OnButtonStart;
                @ButtonStart.canceled += instance.OnButtonStart;
                @buttonRT.started += instance.OnButtonRT;
                @buttonRT.performed += instance.OnButtonRT;
                @buttonRT.canceled += instance.OnButtonRT;
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
        void OnButtonLT(InputAction.CallbackContext context);
        void OnButtonA(InputAction.CallbackContext context);
        void OnButtonY(InputAction.CallbackContext context);
        void OnButtonStart(InputAction.CallbackContext context);
        void OnButtonRT(InputAction.CallbackContext context);
    }
}
