using Duckov.UI;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace DuckovController.SceneEdit.MainGame
{
    public partial class MainGameInputOverride : MonoBehaviour
    {
        private const float aim_smooth_time_fast = 0.05f;

        private const float aim_smooth_time_slow = 0.2f;

        private const float aim_translation = 20f;

        private Vector2 _aimSmoothVel = Vector2.zero;

        private Vector2 _lastDirection;

        private Vector2 _controllerDirection;

        private bool _isGaming;

        private bool _triggerIsDown;

        private bool _isAiming;

        private int _lastUseWeapon;

        //右摇杆平移模式，用于压枪或者是瞄准
        private bool IsTranslateMod => _isAiming || _triggerIsDown;

        private InputManager InputManager => CharacterInputControl.Instance.inputManager;

        private Vector2 AxisCenter => new Vector2(Screen.width / 2f, Screen.height / 2f);

        private float AimDirectDistance => Mathf.Min(Screen.width, Screen.height) * 0.4f;

        private void Awake()
        {
            InitMap();
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
                CharInputCtrlMouseDelta = _controllerDirection * aim_translation;
                _lastDirection = (InputManager.MousePos - AxisCenter).normalized;
            }
            else
            {
                //TODO：整体缺少偏移
                var controllerTarget = AxisCenter + _lastDirection.normalized * AimDirectDistance;
                var cur = InputManager.MousePos;
                //给缓动时间加个权重 向量的模越大响应速度越快
                var time = Mathf.Lerp(aim_smooth_time_slow, aim_smooth_time_fast,
                    Mathf.Clamp01(_controllerDirection.magnitude));
                var next = Vector2.SmoothDamp(cur, controllerTarget, ref _aimSmoothVel, time);
                var delta = next - cur;
                if (delta.magnitude > 0.01f)
                {
                    CharInputCtrlMouseDelta = delta;
                }
            }
        }

        private void OnEnable()
        {
            _map.Enable();
        }

        private void OnDisable()
        {
            _map.Disable();
        }

        private void OnDestroy()
        {
            View.OnActiveViewChanged -= OnActiveViewChanged;
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
            if (MainGameItemTurntableHUD.Instance.Interactive)
            {
                _controllerDirection = Vector2.zero;
                return;
            }
            if (context.performed)
            {
                _controllerDirection = context.ReadValue<Vector2>();
                if (_controllerDirection.magnitude > 0.01f)
                {
                    _lastDirection = _controllerDirection;
                }
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

        private void OnSwitchMeleeOrPutAwayInput(InputAction.CallbackContext context)
        {
            if (context.interaction == null)
            {
                Debug.LogError(
                    $"{Utils.ModName} {nameof(MainGameInputOverride)} {nameof(OnSwitchMeleeOrPutAwayInput)} interaction is null");
                return;
            }
            if (context.interaction.GetType() == typeof(PressInteraction))
            {
                // 切刀
                if (context.started)
                {
                    RecordHoldingWeaponExcludeMelee();
                    CharacterInputControl.Instance.OnPlayerSwitchItemAgentMelee(context);
                }
            }
            else if (context.interaction.GetType() == typeof(HoldInteraction))
            {
                // 收起武器
                if (context.performed)
                {
                    RecordHoldingWeaponExcludeMelee();
                    CharacterInputControl.Instance.OnPutAwayInput(context);
                }
            }
        }

        private void OnSwitchWeaponInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var curWeapon = CharacterMainControl.Main.CurrentHoldItemAgent;
                var primSlot = CharacterMainControl.Main.PrimWeaponSlot().Content;
                var secSlot = CharacterMainControl.Main.SecWeaponSlot().Content;
                if (curWeapon != null)
                {
                    if (primSlot != null && curWeapon.Item == primSlot)
                    {
                        if (CharacterMainControl.Main.SwitchToWeapon(1))
                        {
#if DEBUG
                            Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} SwitchToWeapon 1");
#endif
                            _lastUseWeapon = 1;
                            return;
                        }
                    }
                    if (secSlot != null && curWeapon.Item == secSlot)
                    {
                        if (CharacterMainControl.Main.SwitchToWeapon(0))
                        {
#if DEBUG
                            Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} SwitchToWeapon 0");
#endif
                            _lastUseWeapon = 0;
                            return;
                        }
                    }
                }
#if DEBUG

                Debug.Log($"{Utils.ModName} {nameof(OnSwitchWeaponInput)} RestoreWeapon");
#endif
                if (!CharacterMainControl.Main.SwitchToWeapon(_lastUseWeapon))
                {
                    CharacterMainControl.Main.SwitchToFirstAvailableWeapon();
                }
            }
        }

        private void OnUseItemOrOpenItemTurntableInput(InputAction.CallbackContext context)
        {
            if (context.interaction == null)
            {
                Debug.LogError(
                    $"{Utils.ModName} {nameof(MainGameInputOverride)} {nameof(OnUseItemOrOpenItemTurntableInput)} interaction is null");
                return;
            }
            if (context.interaction.GetType() == typeof(PressInteraction))
            {
                if (context.performed)
                {
                    MainGameItemTurntableHUD.Instance.UseCurrentSlot();
                }
            }
            else if (context.interaction.GetType() == typeof(HoldInteraction))
            {
                // 收起武器
                if (context.performed)
                {
                    MainGameItemTurntableHUD.Instance.Show();
                }
                if (context.canceled)
                {
                    MainGameItemTurntableHUD.Instance.Hide();
                    MainGameItemTurntableHUD.Instance.UseCurrentSlot();
                }
            }
        }

        private void OnMenuInput(InputAction.CallbackContext context)
        {
            PauseMenu.Show();
        }

        // CharacterMainControl.StoreHoldWeaponBeforeUse 主要服务于物品使用返回
        // 再三测试为了这里响应正确，将独立记录使用过的主/副武器
        private void RecordHoldingWeaponExcludeMelee()
        {
            var curWeapon = CharacterMainControl.Main.CurrentHoldItemAgent;
            var primSlot = CharacterMainControl.Main.PrimWeaponSlot().Content;
            var secSlot = CharacterMainControl.Main.SecWeaponSlot().Content;
            if (curWeapon != null)
            {
                if (primSlot != null && curWeapon.Item == primSlot)
                {
                    _lastUseWeapon = 0;
                    return;
                }
                if (secSlot != null && curWeapon.Item == secSlot)
                {
                    _lastUseWeapon = 1;
                }
            }
        }
    }
}
