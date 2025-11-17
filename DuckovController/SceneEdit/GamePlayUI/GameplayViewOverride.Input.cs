using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class GameplayViewOverride
    {
        private InputAction _inputLeftTab;

        private InputAction _inputRightTab;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _inputLeftTab = inputActionMap.AddAction("LeftTab", InputActionType.Button);
            _inputLeftTab.AddBinding(InputSystemUtils.BindingLeftShoulder);

            _inputRightTab = inputActionMap.AddAction("RightTab", InputActionType.Button);
            _inputRightTab.AddBinding(InputSystemUtils.BindingRightShoulder);

            //TODO: 监听问题 和 主游戏冲突
            CancelAction.AddBinding(InputSystemUtils.BindingMenuButton);
        }
    }
}
