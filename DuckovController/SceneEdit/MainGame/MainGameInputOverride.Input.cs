using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameInputOverride
    {
        private InputAction _runAction;

        private InputAction _cancelAction;

        private InputAction _rollAction;

        private InputAction _interactAction;

        private InputAction _reloadAction;

        private InputAction _movementAction;

        private InputAction _aimDirectionAction;

        private InputAction _adsAction;

        private InputAction _triggerAction;

        private InputAction _smallMenuNavigateUp;

        private InputAction _smallMenuNavigateDown;

        // 另外的做法 详见 MainGameInputOverride
        // private InputAction _switchBullet;
        private InputActionSetupExtensions.BindingSyntax _switchBulletBinding;

        private InputAction _switchMeleeOrHold2ToPutAwayWeapon;

        private InputAction _switchWeapon;

        private InputAction _useItemOrHold2OpenTurntable;

        // private InputAction _openMenu;

        private InputAction _openInventory;

        private InputAction _quackAction;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _runAction = inputActionMap.AddAction("Run", InputActionType.Button);
            _runAction.AddBinding(InputSystemUtils.BindingAButton);
            _runAction.AddBinding(InputSystemUtils.BindingLeftStickPress);

            _cancelAction = inputActionMap.AddAction("Cancel", InputActionType.Button);
            _cancelAction.AddBinding(InputSystemUtils.BindingAButton);

            _rollAction = inputActionMap.AddAction("Roll", InputActionType.Button);
            _rollAction.AddBinding(InputSystemUtils.BindingBButton);

            _interactAction = inputActionMap.AddAction("Interact", InputActionType.Button);
            _interactAction.AddBinding(InputSystemUtils.BindingYButton);

            _reloadAction = inputActionMap.AddAction("Reload", InputActionType.Button);
            _reloadAction.AddBinding(InputSystemUtils.BindingXButton);

            _movementAction = inputActionMap.AddAction("Movement", expectedControlLayout: "Vector2");
            _movementAction.AddBinding(InputSystemUtils.BindingLeftStick).WithProcessors("StickDeadzone");

            _aimDirectionAction = inputActionMap.AddAction("AimDirection", expectedControlLayout: "Vector2");
            _aimDirectionAction.AddBinding(InputSystemUtils.BindingRightStick).WithProcessors("StickDeadzone");

            _adsAction = inputActionMap.AddAction("Aim");
            _adsAction.AddBinding(InputSystemUtils.BindingLeftTrigger);

            _triggerAction = inputActionMap.AddAction("Trigger");
            _triggerAction.AddBinding(InputSystemUtils.BindingRightTrigger);

            // 另外的做法 详见 MainGameInputOverride
            // _switchBullet = Map.AddAction("SwitchBullet", InputActionType.Button);
            // _switchBullet.AddBinding(InputSystemUtils.BindingDpadRight);

            _switchMeleeOrHold2ToPutAwayWeapon = inputActionMap
                .AddAction("SwitchMeleeOrPutAway", InputActionType.Button);
            _switchMeleeOrHold2ToPutAwayWeapon.AddBinding(InputSystemUtils.BindingDpadLeft)
                .WithInteractions("Hold,Press");

            _switchWeapon = inputActionMap.AddAction("SwitchWeapon", InputActionType.Button);
            _switchWeapon.AddBinding(InputSystemUtils.BindingLeftShoulder);

            _useItemOrHold2OpenTurntable = inputActionMap
                .AddAction("UseItemOrOpenTurntable", InputActionType.Button);
            _useItemOrHold2OpenTurntable.AddBinding(InputSystemUtils.BindingRightShoulder)
                .WithInteractions("Hold(duration=0.2),Press");

            _smallMenuNavigateUp = inputActionMap.AddAction("SmallMenuNavigationUp", InputActionType.Button);
            _smallMenuNavigateUp.AddBinding(InputSystemUtils.BindingDpadUp);

            _smallMenuNavigateDown = inputActionMap.AddAction("SmallMenuNavigationDown", InputActionType.Button);
            _smallMenuNavigateDown.AddBinding(InputSystemUtils.BindingDpadDown);

            _openInventory = inputActionMap.AddAction("OpenInventory", InputActionType.Button);
            _openInventory.AddBinding(InputSystemUtils.BindingSelectButton);

            // _openMenu = _inputActionMap.AddAction("OpenMenu", InputActionType.Button);
            // _openMenu.AddBinding(InputSystemUtils.BindingMenuButton);

            _quackAction = inputActionMap.AddAction("Quack", InputActionType.Button);
            _quackAction.AddBinding(InputSystemUtils.BindingRightStickPress);

            //TODO:缺少开启夜视仪
            _runAction.BindInput(CharacterInputControl.Instance.OnPlayerRunInput);
            _cancelAction.BindInput(OnCancelInput);
            _rollAction.BindInput(CharacterInputControl.Instance.OnDashInput);
            _reloadAction.BindInput(CharacterInputControl.Instance.OnReloadInput);
            _interactAction.BindInput(CharacterInputControl.Instance.OnInteractInput);
            _movementAction.BindInput(CharacterInputControl.Instance.OnPlayerMoveInput);
            _triggerAction.BindInput(OnTriggerInput);
            _adsAction.BindInput(OnAdsInput);
            _aimDirectionAction.BindInput(OnAimDirectionInput);
            _openInventory.BindInput(CharacterInputControl.Instance.OnUIInventoryInput);
            // _openMenu.BindInput(OnMenuInput);
            _smallMenuNavigateUp.BindInput(OnNavigateUp);
            _smallMenuNavigateDown.BindInput(OnNavigateDown);
            // SwitchBullet.BindInput(OnSwitchBullet);
            _switchMeleeOrHold2ToPutAwayWeapon.BindInput(OnSwitchMeleeOrPutAwayInput);
            _switchWeapon.BindInput(OnSwitchWeaponInput);
            _useItemOrHold2OpenTurntable.BindInput(OnUseItemOrOpenItemTurntableInput);
            _quackAction.BindInput(CharacterInputControl.Instance.OnQuackInput);

            // TODO: 拔掉手柄这里会有问题
            // TODO: 这里缺少注销
            // 这个按键T是业务轮询原有 InputAction 实现的
            // 反射获取这个 Action 额外给这个按钮绑定新的按键
            // 因为原有 PlayerInput=>InputActionMap 带的 ControlScheme 屏蔽了GamePad的输入，在这里要重刷一下
            GameManager.MainPlayerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current, Gamepad.current);
            _switchBulletBinding = SwitchBulletInputAction.AddBinding(InputSystemUtils.BindingDpadRight);
        }
    }
}
