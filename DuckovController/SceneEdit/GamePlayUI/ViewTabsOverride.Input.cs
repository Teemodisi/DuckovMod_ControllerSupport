using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class ViewTabsOverride
    {
        private InputAction _leftAction;

        private InputAction _rightAction;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _leftAction = inputActionMap.AddAction("LeftNavigate", InputActionType.Button);
            _leftAction.AddBinding("<Gamepad>/leftShoulder");

            _rightAction = inputActionMap.AddAction("RightNavigate", InputActionType.Button);
            _rightAction.AddBinding("<Gamepad>/rightShoulder");

            _leftAction.BindInput(OnLeftNavigate);
            _rightAction.BindInput(OnRightNavigate);
        }
    }
}
