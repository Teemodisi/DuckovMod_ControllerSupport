using Duckov.UI;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGameInput
{
    public partial class MainGameInputOverride : MonoBehaviour
    {
        private const float aim_smooth_time = 0.1f;

        private const float aim_translation = 20f;

        private Vector2 _aimSmoothVel = Vector2.zero;

        private Vector2 _lastDirection;

        private Vector2 _controllerDirection;

        private bool _isGaming;

        private bool _triggerIsDown;

        private bool _isAiming;

        private MainGamePlayInputMap _inputMap;

        //右摇杆平移模式，用于压枪或者是瞄准
        private bool IsTranslateMod => _isAiming || _triggerIsDown;

        private InputManager InputManager => CharacterInputControl.Instance.inputManager;

        private Vector2 AxisCenter => new Vector2(Screen.width / 2f, Screen.height / 2f);

        private float AimDirectDistance => Mathf.Min(Screen.width, Screen.height) * 0.4f;

        private void Awake()
        {
            InitInputMap();
            View.OnActiveViewChanged += OnActiveViewChanged;
            OnActiveViewChanged();
        }

        private void LateUpdate()
        {
            if (!_isGaming)
            {
                return;
            }
            if (IsTranslateMod)
            {
                //改为平移
                InputManager.SetAimInputUsingMouse(_controllerDirection * aim_translation);
                _lastDirection = (InputManager.MousePos - AxisCenter).normalized;
            }
            else //TODO：存在漏洞 把连续射击的后坐力给跳过了
            {
                //TODO：整体缺少偏移
                var controllerTarget = AxisCenter + _lastDirection.normalized * AimDirectDistance;
                var cur = InputManager.MousePos;
                var next = Vector2.SmoothDamp(cur, controllerTarget, ref _aimSmoothVel, aim_smooth_time);
                var delta = next - cur;
                if (delta.magnitude > 0.01f)
                {
                    InputManager.SetAimInputUsingMouse(delta);
                }
            }
        }

        private void OnEnable()
        {
            _inputMap.Map.Enable();
        }

        private void OnDisable()
        {
            _inputMap.Map.Disable();
        }

        private void OnDestroy()
        {
            View.OnActiveViewChanged -= OnActiveViewChanged;
        }

        private void InitInputMap()
        {
            //TODO:缺少开启夜视仪
            _inputMap = new MainGamePlayInputMap();
            _inputMap.RunAction.BindInput(CharacterInputControl.Instance.OnPlayerRunInput);
            _inputMap.CancelAction.BindInput(OnCancelInput);
            _inputMap.RollAction.BindInput(CharacterInputControl.Instance.OnDashInput);
            _inputMap.ReloadAction.BindInput(CharacterInputControl.Instance.OnReloadInput);
            _inputMap.InteractAction.BindInput(CharacterInputControl.Instance.OnInteractInput);
            _inputMap.MovementAction.BindInput(CharacterInputControl.Instance.OnPlayerMoveInput);
            _inputMap.TriggerAction.BindInput(OnTriggerInput);
            _inputMap.AdsAction.BindInput(OnAdsInput);
            _inputMap.AimDirectionAction.BindInput(OnAimDirectionInput);
            _inputMap.OpenInventory.BindInput(CharacterInputControl.Instance.OnUIInventoryInput);
            _inputMap.OpenMenu.BindInput(OnMenuInput);
            _inputMap.SmallMenuNavigateUp.BindInput(OnNavigateUp);
            _inputMap.SmallMenuNavigateDown.BindInput(OnNavigateDown);
            // _inputMap.SwitchBullet.BindInput(OnSwitchBullet);
            _inputMap.SwitchMeleeWeapon.BindInput(OnSwitchMeleeInput);
            _inputMap.PutAwayWeapon.BindInput(OnPutAwayInput);
            _inputMap.SwitchWeapon.BindInput(OnSwitchWeaponInput);
            _inputMap.UseItem.BindInput(UseItemInput);
            _inputMap.OpenItemTurntable.BindInput(OnOpenItemTurntableInput);
            _inputMap.QuackAction.BindInput(CharacterInputControl.Instance.OnQuackInput);

            // 这个按键T是业务轮询原有 InputAction 实现的
            // 反射获取这个 Action 额外给这个按钮绑定新的按键
            // 因为原有 InputMap 屏蔽了GamePad的输入，在这里要重刷一下
            GameManager.MainPlayerInput.SwitchCurrentControlScheme(Keyboard.current, Mouse.current, Gamepad.current);
            SwitchBulletInputAction.AddBinding("<Gamepad>/dpad/right");
        }

        private void OnCancelInput(InputAction.CallbackContext context)
        {
            CharacterInputControl.Instance.OnPlayerStopAction(context);
            CharacterInputControl.Instance.OnCancelSkillInput(context);
        }

        private void OnActiveViewChanged()
        {
            _isGaming = View.ActiveView == null;
        }

        private void OnAimDirectionInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _controllerDirection = context.ReadValue<Vector2>();
                _lastDirection = _controllerDirection;
            }
            if (context.canceled)
            {
                _controllerDirection = Vector2.zero;
            }
        }

        private void OnTriggerInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _triggerIsDown = true;
            }
            if (context.canceled)
            {
                _triggerIsDown = false;
            }
            CharacterInputControl.Instance.OnPlayerTriggerInputUsingMouseKeyboard(context);
        }

        private void OnAdsInput(InputAction.CallbackContext context)
        {
            CharacterInputControl.Instance.OnPlayerAdsInput(context);
            if (context.started)
            {
                _isAiming = true;
            }
            if (context.canceled)
            {
                _isAiming = false;
            }
        }

        private void OnNavigateUp(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InputManager.SetSwitchInteractInput(1);
                InputManager.SetSwitchBulletTypeInput(1);
            }
        }

        private void OnNavigateDown(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InputManager.SetSwitchInteractInput(-1);
                InputManager.SetSwitchBulletTypeInput(-1);
            }
        }

        // 收起武器
        private void OnPutAwayInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RecordHoldingWeaponExcludeMelee();
                CharacterInputControl.Instance.OnPutAwayInput(context);
            }
        }

        // 切刀
        private void OnSwitchMeleeInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                RecordHoldingWeaponExcludeMelee();
                CharacterInputControl.Instance.OnPlayerSwitchItemAgentMelee(context);
            }
        }

        private void OnSwitchWeaponInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var curWeapon = CharacterMainControl.Main.CurrentHoldItemAgent;
                var primSlot = CharacterMainControl.Main.PrimWeaponSlot().Content;
                var secSlot = CharacterMainControl.Main.SecWeaponSlot().Content;
                if (curWeapon == null)
                {
#if DEBUG
                    Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} SwitchToFirstAvailableWeapon");
#endif
                    CharacterMainControl.Main.SwitchToFirstAvailableWeapon();
                    return;
                }
                if (primSlot != null && curWeapon.Item == primSlot)
                {
#if DEBUG
                    Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} SwitchToWeapon 1");
#endif
                    CharacterMainControl.Main.SwitchToWeapon(1);
                    return;
                }
                if (secSlot != null && curWeapon.Item == secSlot)
                {
#if DEBUG
                    Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} SwitchToWeapon 0");
#endif
                    CharacterMainControl.Main.SwitchToWeapon(0);
                    return;
                }
#if DEBUG

                Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} RestoreWeapon");
#endif
                CharacterMainControl.Main.SwitchToWeaponBeforeUse();
            }
        }

        private void UseItemInput(InputAction.CallbackContext context) { }

        private void OnOpenItemTurntableInput(InputAction.CallbackContext context) { }

        private void OnMenuInput(InputAction.CallbackContext context)
        {
            PauseMenu.Show();
        }

        private void RecordHoldingWeaponExcludeMelee()
        {
            var curWeapon = CharacterMainControl.Main.CurrentHoldItemAgent;
            var primSlot = CharacterMainControl.Main.PrimWeaponSlot().Content;
            var secSlot = CharacterMainControl.Main.SecWeaponSlot().Content;
            if (curWeapon == null)
            {
                return;
            }
            if (primSlot != null && curWeapon.Item == primSlot)
            {
                RefStoreHoldWeaponBeforeUseMethodInfo();
                return;
            }
            if (secSlot != null && curWeapon.Item == secSlot)
            {
                RefStoreHoldWeaponBeforeUseMethodInfo();
                return;
            }
        }
    }
}
