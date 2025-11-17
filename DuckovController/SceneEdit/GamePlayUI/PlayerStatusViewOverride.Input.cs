using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class PlayerStatusViewOverride
    {
        private InputAction _moveListAction;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _moveListAction = inputActionMap.AddAction("MoveMap");
            _moveListAction.AddBinding(InputSystemUtils.BindingLeftStick);

            _moveListAction.BindInput(OnMoveListInput);
        }
    }
}
