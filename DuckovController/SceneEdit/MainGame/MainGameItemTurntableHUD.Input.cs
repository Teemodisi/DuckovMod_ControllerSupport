using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameItemTurntableHUD
    {
        private InputAction _inputAction;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _inputAction = inputActionMap.AddAction("AimDirection", expectedControlLayout: "Vector2");
            _inputAction.AddBinding("<Gamepad>/rightStick");
            _inputAction.performed += OnRightStickInput;
        }
    }
}
