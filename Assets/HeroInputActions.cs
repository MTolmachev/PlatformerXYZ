// GENERATED AUTOMATICALLY FROM 'Assets/HeroInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HeroInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HeroInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HeroInputActions"",
    ""maps"": [
        {
            ""name"": ""Hero"",
            ""id"": ""5ab8b368-7615-46bb-b15d-f3e3492fea60"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""Value"",
                    ""id"": ""ac33c3fb-dc7d-4237-873c-0bbc89d9424c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d05a69e8-47a6-4094-8d9a-485141f895b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8d3470e5-5e69-48c6-9ee9-3768e08476b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""4ce4e1dd-9b19-4744-a0cf-dadbe936df23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""278450af-1f6f-437a-b62b-0811e13ababc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePotion"",
                    ""type"": ""Button"",
                    ""id"": ""f760b98a-2c1b-4927-8014-899139356205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""e347014a-e68d-4235-8962-f599129c75b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextItem"",
                    ""type"": ""Button"",
                    ""id"": ""1060fadd-1a92-44e0-8110-751c9de654bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""18667174-eb21-4e07-96bd-0899a423470a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cf845f26-9618-4ba4-abab-58d1f6887963"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""46666c93-8d8d-4516-a430-63f8c2201dff"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""80077146-5311-4eb3-a497-4fe7e680d688"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""01c92b0b-ceae-437c-8a97-5a3c274c0c8e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""63a9ef65-b964-42c7-86a6-d6032230534b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6af4413c-e409-4c56-a3ba-41ac04ee7370"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e74e1f94-f3ac-4f32-9c4e-931256925199"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dbcc54a0-4dbb-440d-a525-f66dea3ec0d9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8094df93-c68a-422b-b3ee-82c47d5b372b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c360b769-f792-433e-9c75-06a44cd436af"",
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
                    ""id"": ""7d383dd0-3cfe-4f09-a587-d5cb174acedf"",
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
                    ""id"": ""66f8839e-1971-4868-a7e9-b26cc6f70185"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6c1ff54-7e0c-4161-82c8-f83e3ccb2332"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6470cc7a-af11-447e-9613-4056febfd8f3"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bcb6ed1-d3ab-4b7a-80e3-23e28079877d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ef32d0b-5ff4-4411-91c5-1403950d01df"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Hero
        m_Hero = asset.FindActionMap("Hero", throwIfNotFound: true);
        m_Hero_HorizontalMovement = m_Hero.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_Hero_Jump = m_Hero.FindAction("Jump", throwIfNotFound: true);
        m_Hero_Interact = m_Hero.FindAction("Interact", throwIfNotFound: true);
        m_Hero_Attack = m_Hero.FindAction("Attack", throwIfNotFound: true);
        m_Hero_Throw = m_Hero.FindAction("Throw", throwIfNotFound: true);
        m_Hero_UsePotion = m_Hero.FindAction("UsePotion", throwIfNotFound: true);
        m_Hero_Pause = m_Hero.FindAction("Pause", throwIfNotFound: true);
        m_Hero_NextItem = m_Hero.FindAction("NextItem", throwIfNotFound: true);
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

    // Hero
    private readonly InputActionMap m_Hero;
    private IHeroActions m_HeroActionsCallbackInterface;
    private readonly InputAction m_Hero_HorizontalMovement;
    private readonly InputAction m_Hero_Jump;
    private readonly InputAction m_Hero_Interact;
    private readonly InputAction m_Hero_Attack;
    private readonly InputAction m_Hero_Throw;
    private readonly InputAction m_Hero_UsePotion;
    private readonly InputAction m_Hero_Pause;
    private readonly InputAction m_Hero_NextItem;
    public struct HeroActions
    {
        private @HeroInputActions m_Wrapper;
        public HeroActions(@HeroInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_Hero_HorizontalMovement;
        public InputAction @Jump => m_Wrapper.m_Hero_Jump;
        public InputAction @Interact => m_Wrapper.m_Hero_Interact;
        public InputAction @Attack => m_Wrapper.m_Hero_Attack;
        public InputAction @Throw => m_Wrapper.m_Hero_Throw;
        public InputAction @UsePotion => m_Wrapper.m_Hero_UsePotion;
        public InputAction @Pause => m_Wrapper.m_Hero_Pause;
        public InputAction @NextItem => m_Wrapper.m_Hero_NextItem;
        public InputActionMap Get() { return m_Wrapper.m_Hero; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HeroActions set) { return set.Get(); }
        public void SetCallbacks(IHeroActions instance)
        {
            if (m_Wrapper.m_HeroActionsCallbackInterface != null)
            {
                @HorizontalMovement.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnHorizontalMovement;
                @Jump.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnJump;
                @Interact.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnAttack;
                @Throw.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnThrow;
                @UsePotion.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePotion;
                @UsePotion.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePotion;
                @UsePotion.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnUsePotion;
                @Pause.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnPause;
                @NextItem.started -= m_Wrapper.m_HeroActionsCallbackInterface.OnNextItem;
                @NextItem.performed -= m_Wrapper.m_HeroActionsCallbackInterface.OnNextItem;
                @NextItem.canceled -= m_Wrapper.m_HeroActionsCallbackInterface.OnNextItem;
            }
            m_Wrapper.m_HeroActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMovement.started += instance.OnHorizontalMovement;
                @HorizontalMovement.performed += instance.OnHorizontalMovement;
                @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @UsePotion.started += instance.OnUsePotion;
                @UsePotion.performed += instance.OnUsePotion;
                @UsePotion.canceled += instance.OnUsePotion;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @NextItem.started += instance.OnNextItem;
                @NextItem.performed += instance.OnNextItem;
                @NextItem.canceled += instance.OnNextItem;
            }
        }
    }
    public HeroActions @Hero => new HeroActions(this);
    public interface IHeroActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnUsePotion(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnNextItem(InputAction.CallbackContext context);
    }
}
