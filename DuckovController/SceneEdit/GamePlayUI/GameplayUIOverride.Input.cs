using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class GameplayUIOverride
    {
        private InputActionMap _inputActionMap;

        private InputAction _inputLeftTab;

        private InputAction _inputRightTab;

        private void InitInputAction()
        {
            _inputActionMap = new InputActionMap("GamePlayUIGamePad");
            
            _inputLeftTab = _inputActionMap.AddAction("LeftTab", InputActionType.Button);
            _inputLeftTab.AddBinding("<Gamepad>/leftShoulder");
            
            _inputRightTab = _inputActionMap.AddAction("RightTab", InputActionType.Button);
            _inputRightTab.AddBinding("<Gamepad>/rightShoulder");
            
            CancelAction.AddBinding("<Gamepad>/buttonEast");
        }

        private void RegInput()
        {
            _inputActionMap.Enable();
        }

        private void UnRegInput()
        {
            _inputActionMap.Disable();
        }
    }
}
