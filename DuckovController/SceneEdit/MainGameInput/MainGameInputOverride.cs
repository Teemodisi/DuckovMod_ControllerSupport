using System;
using System.Reflection;
using Duckov.UI;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainGameInput
{
    public class MainGameInputOverride : MonoBehaviour
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

        private BulletTypeHUD _bulletTypeHUD;

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
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Debug.Log("=====");
                foreach (var inputBinding in SwitchBulletInputAction.bindings)
                {
                    Debug.Log(inputBinding.path);
                }
            }

            if (SwitchBulletInputAction.WasPressedThisFrame())
            {
                Debug.Log("=============");
            }

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
            _inputMap.SwitchMeleeWeapon.BindInput(CharacterInputControl.Instance.OnPlayerSwitchItemAgentMelee);
            _inputMap.PutAwayWeapon.BindInput(CharacterInputControl.Instance.OnPutAwayInput);
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

        private void OnSwitchWeaponInput(InputAction.CallbackContext context) { }

        private void UseItemInput(InputAction.CallbackContext context) { }

        private void OnOpenItemTurntableInput(InputAction.CallbackContext context) { }

        private void OnMenuInput(InputAction.CallbackContext context)
        {
            PauseMenu.Show();
        }

    #region Reflection

        private FieldInfo GameCameraAimingTypeFieldInfo { get; } =
            ReflectionUtils.FindField<GameCamera>("cameraAimingType");

        private GameCamera.CameraAimingTypes CameraAimingType =>
            (GameCamera.CameraAimingTypes)GameCameraAimingTypeFieldInfo.GetValue(GameCamera.Instance);

        //Typo bro
        private FieldInfo CharInputScrollYFieldInfo { get; } =
            ReflectionUtils.FindField<CharacterInputControl>("scollY");

        private float CharInputScrollY
        {
            get => (float)CharInputScrollYFieldInfo.GetValue(CharacterInputControl.Instance);
            set => CharInputScrollYFieldInfo.SetValue(CharacterInputControl.Instance, value);
        }

        private FieldInfo _switchBulletTypeFieldInfo;

        private FieldInfo CicInputActionsFieldInfo { get; } =
            ReflectionUtils.FindField<CharacterInputControl>("inputActions");

        private InputAction _switchBulletInputAction;

        //damn 这个交互怎么是这样捏
        private InputAction SwitchBulletInputAction
        {
            get
            {
                if (_switchBulletInputAction == null)
                {
                    object fieldObj = null;
                    if (_switchBulletTypeFieldInfo == null)
                    {
                        fieldObj = CicInputActionsFieldInfo.GetValue(CharacterInputControl.Instance);
                        var priType = fieldObj.GetType();
                        _switchBulletTypeFieldInfo = priType.GetField("SwitchBulletType",
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                        if (_switchBulletTypeFieldInfo == null)
                        {
                            throw new Exception("找不到SwitchBulletInputAction");
                        }
                    }
                    if (fieldObj == null)
                    {
                        fieldObj = CicInputActionsFieldInfo.GetValue(CharacterInputControl.Instance);
                    }
                    _switchBulletInputAction =
                        _switchBulletTypeFieldInfo.GetValue(fieldObj) as InputAction;
                }
                return _switchBulletInputAction;
            }
        }

    #endregion
    }
}
