using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class PlayerStatusViewOverride
    {
        private InputActionMap _inputActionMap;

        private InputAction _moveListAction;
        
        private void InitInput()
        {
            _inputActionMap = new InputActionMap(nameof(PlayerStatusViewOverride));
            
            _moveListAction = _inputActionMap.AddAction("MoveMap");
            _moveListAction.AddBinding("<Gamepad>/leftStick");
            
            _moveListAction.BindInput(OnMoveListInput);
        }
    }
}
