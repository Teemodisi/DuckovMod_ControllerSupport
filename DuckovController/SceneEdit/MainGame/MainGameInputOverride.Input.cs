using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameInputOverride
    {
        private InputActionMap _inputActionMap;

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

        private InputAction _switchMeleeOrHold2ToPutAwayWeapon;

        private InputAction _switchWeapon;

        private InputAction _useItemOrHold2OpenTurntable;

        private InputAction _openMenu;

        private InputAction _openInventory;

        private InputAction _quackAction;

        private void InitMap()
        {
            _inputActionMap = new InputActionMap(nameof(MainGameInputOverride));

            _runAction = _inputActionMap.AddAction("Run", InputActionType.Button);
            _runAction.AddBinding("<Gamepad>/buttonEast");
            _runAction.AddBinding("<Gamepad>/leftStickPress");

            _cancelAction = _inputActionMap.AddAction("Cancel", InputActionType.Button);
            _cancelAction.AddBinding("<Gamepad>/buttonEast");

            _rollAction = _inputActionMap.AddAction("Roll", InputActionType.Button);
            _rollAction.AddBinding("<Gamepad>/buttonSouth");

            _interactAction = _inputActionMap.AddAction("Interact", InputActionType.Button);
            _interactAction.AddBinding("<Gamepad>/buttonNorth");

            _reloadAction = _inputActionMap.AddAction("Reload", InputActionType.Button);
            _reloadAction.AddBinding("<Gamepad>/buttonWest");

            _movementAction = _inputActionMap.AddAction("Movement", expectedControlLayout: "Vector2");
            _movementAction.AddBinding("<Gamepad>/leftStick").WithProcessors("StickDeadzone");

            _aimDirectionAction = _inputActionMap.AddAction("AimDirection", expectedControlLayout: "Vector2");
            _aimDirectionAction.AddBinding("<Gamepad>/rightStick").WithProcessors("StickDeadzone");

            _adsAction = _inputActionMap.AddAction("Aim");
            _adsAction.AddBinding("<Gamepad>/leftTrigger");

            _triggerAction = _inputActionMap.AddAction("Trigger");
            _triggerAction.AddBinding("<Gamepad>/rightTrigger");

            // 另外的做法 详见 MainGameInputOverride
            // _switchBullet = Map.AddAction("SwitchBullet", InputActionType.Button);
            // _switchBullet.AddBinding("<Gamepad>/dpad/right");

            _switchMeleeOrHold2ToPutAwayWeapon = _inputActionMap
                .AddAction("SwitchMeleeOrPutAway", InputActionType.Button);
            _switchMeleeOrHold2ToPutAwayWeapon.AddBinding("<Gamepad>/dpad/left")
                .WithInteractions("Hold,Press");

            _switchWeapon = _inputActionMap.AddAction("SwitchWeapon", InputActionType.Button);
            _switchWeapon.AddBinding("<Gamepad>/leftShoulder");

            _useItemOrHold2OpenTurntable = _inputActionMap
                .AddAction("UseItemOrOpenTurntable", InputActionType.Button);
            _useItemOrHold2OpenTurntable.AddBinding("<Gamepad>/rightShoulder")
                .WithInteractions("Hold(duration=0.2),Press");

            _smallMenuNavigateUp = _inputActionMap.AddAction("SmallMenuNavigationUp", InputActionType.Button);
            _smallMenuNavigateUp.AddBinding("<Gamepad>/dpad/up");

            _smallMenuNavigateDown = _inputActionMap.AddAction("SmallMenuNavigationDown", InputActionType.Button);
            _smallMenuNavigateDown.AddBinding("<Gamepad>/dpad/down");

            _openInventory = _inputActionMap.AddAction("OpenInventory", InputActionType.Button);
            _openInventory.AddBinding("<Gamepad>/select");

            _openMenu = _inputActionMap.AddAction("OpenMenu", InputActionType.Button);
            _openMenu.AddBinding("<Gamepad>/start");

            _quackAction = _inputActionMap.AddAction("Quack", InputActionType.Button);
            _quackAction.AddBinding("<Gamepad>/rightStickPress");

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
            _openMenu.BindInput(OnMenuInput);
            _smallMenuNavigateUp.BindInput(OnNavigateUp);
            _smallMenuNavigateDown.BindInput(OnNavigateDown);
            // SwitchBullet.BindInput(OnSwitchBullet);
            _switchMeleeOrHold2ToPutAwayWeapon.BindInput(OnSwitchMeleeOrPutAwayInput);
            _switchWeapon.BindInput(OnSwitchWeaponInput);
            _useItemOrHold2OpenTurntable.BindInput(OnUseItemOrOpenItemTurntableInput);
            _quackAction.BindInput(CharacterInputControl.Instance.OnQuackInput);

            // TODO：拔掉手柄这里会有问题
            // 这个按键T是业务轮询原有 InputAction 实现的
            // 反射获取这个 Action 额外给这个按钮绑定新的按键
            // 因为原有 PlayerInput=>InputActionMap 带的 ControlScheme 屏蔽了GamePad的输入，在这里要重刷一下
            GameManager.MainPlayerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current, Gamepad.current);
            SwitchBulletInputAction.AddBinding("<Gamepad>/dpad/right");
        }
    }
}
