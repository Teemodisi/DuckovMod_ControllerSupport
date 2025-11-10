using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController
{
    public class GamePadInput : SingletonMonoBehaviour<GamePadInput>
    {
        private InputActionMap _inputActionMap;

        public InputAction NavigateUpAction { get; private set; }

        public InputAction NavigateDownAction { get; private set; }

        public InputAction ConfirmAction { get; private set; }

        public InputAction CancelAction { get; private set; }

        public InputAction RunAction { get; private set; }

        public InputAction RollAction { get; private set; }

        public InputAction InteractAction { get; private set; }

        public InputAction ReloadAction { get; private set; }

        public InputAction MovementAction { get; private set; }

        public InputAction AimDirectionAction { get; private set; }
        
        public InputAction AimAction { get; private set; }
        
        public InputAction TriggerAction { get; private set; }
        
        
        public InputAction OpenMapAction {get; private set; }
        
        public InputAction OpenInventory { get; private set; }
        public InputAction QuackAction { get; private set; }
        

        protected override void Awake()
        {
            base.Awake();
            InitGamePadInputAction();
        }

        private void OnEnable()
        {
            _inputActionMap.Enable();
        }

        private void OnDisable()
        {
            _inputActionMap.Disable();
        }

        private void InitGamePadInputAction()
        {
            _inputActionMap = new InputActionMap("GamepadControls");
            ConfirmAction = _inputActionMap.AddAction("Confirm", InputActionType.Button);
            ConfirmAction.AddBinding("<Gamepad>/buttonSouth");

            CancelAction = _inputActionMap.AddAction("Cancel", InputActionType.Button);
            CancelAction.AddBinding("<Gamepad>/buttonEast");

            NavigateUpAction = _inputActionMap.AddAction("NavigationUp", InputActionType.Button);
            NavigateUpAction.AddBinding("<Gamepad>/dpad/up");

            NavigateDownAction = _inputActionMap.AddAction("NavigationDown", InputActionType.Button);
            NavigateDownAction.AddBinding("<Gamepad>/dpad/down");

            RunAction = _inputActionMap.AddAction("Run", InputActionType.Button);
            RunAction.AddBinding("<Gamepad>/buttonSouth");
            
            RollAction = _inputActionMap.AddAction("Roll", InputActionType.Button);
            RollAction.AddBinding("<Gamepad>/buttonEast");
            
            InteractAction = _inputActionMap.AddAction("Interact", InputActionType.Button);
            InteractAction.AddBinding("<Gamepad>/buttonWest");
            
            ReloadAction = _inputActionMap.AddAction("Reload", InputActionType.Button);
            ReloadAction.AddBinding("<Gamepad>/buttonNorth");
            
            MovementAction = _inputActionMap.AddAction("Movement");
            MovementAction.AddBinding("<Gamepad>/leftStick");

            AimDirectionAction = _inputActionMap.AddAction("AimDirection");
            AimDirectionAction.AddBinding("<Gamepad>/rightStick");

            AimAction = _inputActionMap.AddAction("Aim", InputActionType.Button);
            AimAction.AddBinding("<Gamepad>/leftTrigger");
            
            TriggerAction = _inputActionMap.AddAction("Trigger", InputActionType.Button);
            TriggerAction.AddBinding("<Gamepad>/rightTrigger");
            
            QuackAction = _inputActionMap.AddAction("Quack", InputActionType.Button);
            QuackAction.AddBinding("<Gamepad>/rightStickPress");
        }
    }
}
