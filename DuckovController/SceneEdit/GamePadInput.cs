using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit
{
    public class GamePadInput : SingletonMonoBehaviour<GamePadInput>
    {
        private InputActionMap _inputActionMap;

        public InputAction NavigateUpAction { get; private set; }

        public InputAction NavigateDownAction { get; private set; }

        public InputAction ConfirmAction { get; private set; }

        public InputAction CancelAction { get; private set; }

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
        }
    }
}
