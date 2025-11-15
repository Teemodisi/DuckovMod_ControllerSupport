using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class ViewTabsOverride
    {
        private InputActionMap _inputActionMap;

        private InputAction _leftAction;

        private InputAction _rightAction;

        private void InitInput()
        {
            _inputActionMap = new InputActionMap(nameof(ViewTabsOverride));
            _leftAction = _inputActionMap.AddAction("LeftNavigate", InputActionType.Button);
            _leftAction.AddBinding("<Gamepad>/leftShoulder");

            _rightAction = _inputActionMap.AddAction("RightNavigate", InputActionType.Button);
            _rightAction.AddBinding("<Gamepad>/rightShoulder");

            _leftAction.BindInput(OnLeftNavigate);
            _rightAction.BindInput(OnRightNavigate);
        }
    }
}
