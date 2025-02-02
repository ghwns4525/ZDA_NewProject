// GENERATED AUTOMATICALLY FROM 'Assets/2_SC/Player/Script/PlayerCtrl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerCtrl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerCtrl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerCtrl"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""42802b6e-26fb-4925-8e26-021e54603554"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9c4a8324-8622-4eeb-b0fc-e58684d9f4ac"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""82428ef8-8274-4b57-910e-d94fc2dd58c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""b76e8f14-6f22-4338-968a-53c00c60f59b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""9020fdcc-c82d-4380-9908-546dcb34e3ce"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ffb90438-c28d-4984-84f3-68605159782b"",
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
                    ""id"": ""c7c600b0-4bef-4c99-95bd-e4906fec2360"",
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
                    ""id"": ""3fb54c91-884e-4e10-bcee-b91af026ce58"",
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
                    ""id"": ""57d3c9fc-8eb5-4b93-93f4-e20efda06927"",
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
                    ""id"": ""93b9fc20-e5f5-4ade-beec-d54e4043037c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6dace5d5-3059-4cf6-b2ab-95312e2e05a4"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""280fa47c-9d8d-4fc9-b9ad-3652ee17d9dd"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""e94dca09-3ebf-4568-9bb1-237bfc0df6b1"",
            ""actions"": [
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""4563eb25-ee7f-48e0-b0a0-c9fae101f23c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RB"",
                    ""type"": ""Button"",
                    ""id"": ""4af1ba33-1203-408c-a7e3-baf880379922"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RT"",
                    ""type"": ""Button"",
                    ""id"": ""c38ac1c0-12b7-43a4-a7f8-87dcf96e6a86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FixedCamera"",
                    ""type"": ""Button"",
                    ""id"": ""700e22c0-b56d-431f-96cc-a15fc92fabea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FixedCameraWheelUpDown"",
                    ""type"": ""PassThrough"",
                    ""id"": ""644d06f7-2708-435b-9264-d39c0b023eff"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""33ab44be-3cc0-495b-994d-a7cceab39821"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""222a6906-cf56-46f5-b6ed-f055d9e0101c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9b536ce-6114-4847-b447-37d99c0ce419"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0aae32b-7f61-4c68-846c-f2dd8ac1664c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b68b81b-36d0-4c72-9e8a-bfcc12227253"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d92e794e-8d86-4ea8-b0ea-538c97835906"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FixedCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f459ba24-6d2b-411b-bd3d-16be6e615ed6"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FixedCameraWheelUpDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cfc702b-7063-4aab-b525-563c266cdb72"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Test"",
            ""id"": ""48c0c9ff-e843-4a40-a89f-65c9a1d70705"",
            ""actions"": [
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""37514d2c-80c8-4e33-9812-ccb4e78c2ed3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shock_Test"",
                    ""type"": ""Button"",
                    ""id"": ""6af98cd7-132b-44f0-a5f1-985a848e641b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6dcc75ef-7592-459e-8e9c-ea07cee8c32a"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d4fdead-095a-44b0-8bd2-3cc74aac6957"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shock_Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""cc96a109-c44c-4cdb-9340-bbcb7e8de39e"",
            ""actions"": [
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""60cfb1eb-2bb1-40bc-a2d1-7a362def1f39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_F"",
                    ""type"": ""Button"",
                    ""id"": ""b1e68e76-d972-4f9d-9167-ac261c40fefc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_G"",
                    ""type"": ""Button"",
                    ""id"": ""5b54ec84-6274-4ef9-b055-212f8e5234fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_Q"",
                    ""type"": ""Button"",
                    ""id"": ""54de1ee1-7697-4247-9948-b3a1abdb5ece"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_E"",
                    ""type"": ""Button"",
                    ""id"": ""19a5f3cd-6ef9-425a-80ec-1ff56af210e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_R"",
                    ""type"": ""Button"",
                    ""id"": ""640eca32-5330-42c2-8e56-f22a50cb99d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_Up"",
                    ""type"": ""Button"",
                    ""id"": ""445fca51-7dc6-41da-97ab-334c1c88b92d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_Down"",
                    ""type"": ""Button"",
                    ""id"": ""754c861e-de9d-46df-929e-2844d654f8f2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test_Anykey"",
                    ""type"": ""Button"",
                    ""id"": ""ee74fa8e-24f8-4786-9563-0afbb0356180"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""95b7e3ea-1e41-4a97-a4d7-77b905b555f2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d322ee81-750f-40f3-bf83-ddcf6de815a1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd05d9d7-86df-4b64-88ba-d05d1ee7a963"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_G"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e11508d4-e1f8-4f9d-b0f4-c7862430bf78"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_Q"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d357556-b420-4d88-886c-6b4f7ec4cbf4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08874238-2fd5-4c67-a6fb-303c240cd5a3"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bab4e5e-f5c4-4e53-a6e9-713136f6d0e3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e91121fc-9cd7-4e8a-80d3-147588f49513"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14caf109-7c4b-4bed-a862-816fd42da2a6"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_Anykey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        m_PlayerMovement_Sprint = m_PlayerMovement.FindAction("Sprint", throwIfNotFound: true);
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Roll = m_PlayerActions.FindAction("Roll", throwIfNotFound: true);
        m_PlayerActions_RB = m_PlayerActions.FindAction("RB", throwIfNotFound: true);
        m_PlayerActions_RT = m_PlayerActions.FindAction("RT", throwIfNotFound: true);
        m_PlayerActions_FixedCamera = m_PlayerActions.FindAction("FixedCamera", throwIfNotFound: true);
        m_PlayerActions_FixedCameraWheelUpDown = m_PlayerActions.FindAction("FixedCameraWheelUpDown", throwIfNotFound: true);
        m_PlayerActions_Reload = m_PlayerActions.FindAction("Reload", throwIfNotFound: true);
        // Test
        m_Test = asset.FindActionMap("Test", throwIfNotFound: true);
        m_Test_Test = m_Test.FindAction("Test", throwIfNotFound: true);
        m_Test_Shock_Test = m_Test.FindAction("Shock_Test", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Menu = m_UI.FindAction("Menu", throwIfNotFound: true);
        m_UI_Test_F = m_UI.FindAction("Test_F", throwIfNotFound: true);
        m_UI_Test_G = m_UI.FindAction("Test_G", throwIfNotFound: true);
        m_UI_Test_Q = m_UI.FindAction("Test_Q", throwIfNotFound: true);
        m_UI_Test_E = m_UI.FindAction("Test_E", throwIfNotFound: true);
        m_UI_Test_R = m_UI.FindAction("Test_R", throwIfNotFound: true);
        m_UI_Test_Up = m_UI.FindAction("Test_Up", throwIfNotFound: true);
        m_UI_Test_Down = m_UI.FindAction("Test_Down", throwIfNotFound: true);
        m_UI_Test_Anykey = m_UI.FindAction("Test_Anykey", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    private readonly InputAction m_PlayerMovement_Sprint;
    public struct PlayerMovementActions
    {
        private @PlayerCtrl m_Wrapper;
        public PlayerMovementActions(@PlayerCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputAction @Sprint => m_Wrapper.m_PlayerMovement_Sprint;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Sprint.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSprint;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Roll;
    private readonly InputAction m_PlayerActions_RB;
    private readonly InputAction m_PlayerActions_RT;
    private readonly InputAction m_PlayerActions_FixedCamera;
    private readonly InputAction m_PlayerActions_FixedCameraWheelUpDown;
    private readonly InputAction m_PlayerActions_Reload;
    public struct PlayerActionsActions
    {
        private @PlayerCtrl m_Wrapper;
        public PlayerActionsActions(@PlayerCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Roll => m_Wrapper.m_PlayerActions_Roll;
        public InputAction @RB => m_Wrapper.m_PlayerActions_RB;
        public InputAction @RT => m_Wrapper.m_PlayerActions_RT;
        public InputAction @FixedCamera => m_Wrapper.m_PlayerActions_FixedCamera;
        public InputAction @FixedCameraWheelUpDown => m_Wrapper.m_PlayerActions_FixedCameraWheelUpDown;
        public InputAction @Reload => m_Wrapper.m_PlayerActions_Reload;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Roll.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRoll;
                @RB.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRB;
                @RB.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRB;
                @RB.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRB;
                @RT.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRT;
                @RT.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRT;
                @RT.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRT;
                @FixedCamera.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFixedCamera;
                @FixedCamera.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFixedCamera;
                @FixedCamera.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFixedCamera;
                @FixedCameraWheelUpDown.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFixedCameraWheelUpDown;
                @FixedCameraWheelUpDown.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFixedCameraWheelUpDown;
                @FixedCameraWheelUpDown.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFixedCameraWheelUpDown;
                @Reload.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @RB.started += instance.OnRB;
                @RB.performed += instance.OnRB;
                @RB.canceled += instance.OnRB;
                @RT.started += instance.OnRT;
                @RT.performed += instance.OnRT;
                @RT.canceled += instance.OnRT;
                @FixedCamera.started += instance.OnFixedCamera;
                @FixedCamera.performed += instance.OnFixedCamera;
                @FixedCamera.canceled += instance.OnFixedCamera;
                @FixedCameraWheelUpDown.started += instance.OnFixedCameraWheelUpDown;
                @FixedCameraWheelUpDown.performed += instance.OnFixedCameraWheelUpDown;
                @FixedCameraWheelUpDown.canceled += instance.OnFixedCameraWheelUpDown;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // Test
    private readonly InputActionMap m_Test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_Test_Test;
    private readonly InputAction m_Test_Shock_Test;
    public struct TestActions
    {
        private @PlayerCtrl m_Wrapper;
        public TestActions(@PlayerCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test => m_Wrapper.m_Test_Test;
        public InputAction @Shock_Test => m_Wrapper.m_Test_Shock_Test;
        public InputActionMap Get() { return m_Wrapper.m_Test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @Test.started -= m_Wrapper.m_TestActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnTest;
                @Shock_Test.started -= m_Wrapper.m_TestActionsCallbackInterface.OnShock_Test;
                @Shock_Test.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnShock_Test;
                @Shock_Test.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnShock_Test;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
                @Shock_Test.started += instance.OnShock_Test;
                @Shock_Test.performed += instance.OnShock_Test;
                @Shock_Test.canceled += instance.OnShock_Test;
            }
        }
    }
    public TestActions @Test => new TestActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Menu;
    private readonly InputAction m_UI_Test_F;
    private readonly InputAction m_UI_Test_G;
    private readonly InputAction m_UI_Test_Q;
    private readonly InputAction m_UI_Test_E;
    private readonly InputAction m_UI_Test_R;
    private readonly InputAction m_UI_Test_Up;
    private readonly InputAction m_UI_Test_Down;
    private readonly InputAction m_UI_Test_Anykey;
    public struct UIActions
    {
        private @PlayerCtrl m_Wrapper;
        public UIActions(@PlayerCtrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menu => m_Wrapper.m_UI_Menu;
        public InputAction @Test_F => m_Wrapper.m_UI_Test_F;
        public InputAction @Test_G => m_Wrapper.m_UI_Test_G;
        public InputAction @Test_Q => m_Wrapper.m_UI_Test_Q;
        public InputAction @Test_E => m_Wrapper.m_UI_Test_E;
        public InputAction @Test_R => m_Wrapper.m_UI_Test_R;
        public InputAction @Test_Up => m_Wrapper.m_UI_Test_Up;
        public InputAction @Test_Down => m_Wrapper.m_UI_Test_Down;
        public InputAction @Test_Anykey => m_Wrapper.m_UI_Test_Anykey;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Menu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Test_F.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_F;
                @Test_F.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_F;
                @Test_F.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_F;
                @Test_G.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_G;
                @Test_G.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_G;
                @Test_G.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_G;
                @Test_Q.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Q;
                @Test_Q.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Q;
                @Test_Q.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Q;
                @Test_E.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_E;
                @Test_E.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_E;
                @Test_E.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_E;
                @Test_R.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_R;
                @Test_R.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_R;
                @Test_R.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_R;
                @Test_Up.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Up;
                @Test_Up.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Up;
                @Test_Up.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Up;
                @Test_Down.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Down;
                @Test_Down.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Down;
                @Test_Down.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Down;
                @Test_Anykey.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Anykey;
                @Test_Anykey.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Anykey;
                @Test_Anykey.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTest_Anykey;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Test_F.started += instance.OnTest_F;
                @Test_F.performed += instance.OnTest_F;
                @Test_F.canceled += instance.OnTest_F;
                @Test_G.started += instance.OnTest_G;
                @Test_G.performed += instance.OnTest_G;
                @Test_G.canceled += instance.OnTest_G;
                @Test_Q.started += instance.OnTest_Q;
                @Test_Q.performed += instance.OnTest_Q;
                @Test_Q.canceled += instance.OnTest_Q;
                @Test_E.started += instance.OnTest_E;
                @Test_E.performed += instance.OnTest_E;
                @Test_E.canceled += instance.OnTest_E;
                @Test_R.started += instance.OnTest_R;
                @Test_R.performed += instance.OnTest_R;
                @Test_R.canceled += instance.OnTest_R;
                @Test_Up.started += instance.OnTest_Up;
                @Test_Up.performed += instance.OnTest_Up;
                @Test_Up.canceled += instance.OnTest_Up;
                @Test_Down.started += instance.OnTest_Down;
                @Test_Down.performed += instance.OnTest_Down;
                @Test_Down.canceled += instance.OnTest_Down;
                @Test_Anykey.started += instance.OnTest_Anykey;
                @Test_Anykey.performed += instance.OnTest_Anykey;
                @Test_Anykey.canceled += instance.OnTest_Anykey;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnRoll(InputAction.CallbackContext context);
        void OnRB(InputAction.CallbackContext context);
        void OnRT(InputAction.CallbackContext context);
        void OnFixedCamera(InputAction.CallbackContext context);
        void OnFixedCameraWheelUpDown(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface ITestActions
    {
        void OnTest(InputAction.CallbackContext context);
        void OnShock_Test(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMenu(InputAction.CallbackContext context);
        void OnTest_F(InputAction.CallbackContext context);
        void OnTest_G(InputAction.CallbackContext context);
        void OnTest_Q(InputAction.CallbackContext context);
        void OnTest_E(InputAction.CallbackContext context);
        void OnTest_R(InputAction.CallbackContext context);
        void OnTest_Up(InputAction.CallbackContext context);
        void OnTest_Down(InputAction.CallbackContext context);
        void OnTest_Anykey(InputAction.CallbackContext context);
    }
}
