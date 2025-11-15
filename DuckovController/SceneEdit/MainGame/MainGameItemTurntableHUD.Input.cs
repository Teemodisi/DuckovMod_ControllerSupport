using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameItemTurntableHUD
    {
        private InputAction _inputAction;

        private void InitInputAction()
        {
            _inputAction = new InputAction("AimDirection", expectedControlType: "Vector2");
            _inputAction.AddBinding("<Gamepad>/rightStick");
            _inputAction.performed += OnRightStickInput;
        }
    }
}
