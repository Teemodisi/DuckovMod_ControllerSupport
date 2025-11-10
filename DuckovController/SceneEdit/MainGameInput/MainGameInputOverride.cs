using System;
using System.Collections.Generic;
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

        private Dictionary<InputAction, Action<InputAction.CallbackContext>> _bindingMap;

        private bool _hadBind;

        private Vector2 _lastDirection;

        private Vector2 _controllerDirection;

        private bool _isGaming;

        private bool _triggerIsDown;

        private bool _isAiming;

        //右摇杆平移模式，用于压枪或者是瞄准
        private bool IsTranslateMod => _isAiming || _triggerIsDown;

        private InputManager InputManager => CharacterInputControl.Instance.inputManager;

        private Vector2 AxisCenter => new Vector2(Screen.width / 2f, Screen.height / 2f);

        private float AimDirectDistance => Mathf.Min(Screen.width, Screen.height) * 0.4f;

        private void Awake()
        {
            _bindingMap = new Dictionary<InputAction, Action<InputAction.CallbackContext>>
            {
                { GamePadInput.Instance.RunAction, CharacterInputControl.Instance.OnPlayerRunInput },
                { GamePadInput.Instance.RollAction, CharacterInputControl.Instance.OnDashInput },
                { GamePadInput.Instance.ReloadAction, CharacterInputControl.Instance.OnReloadInput },
                { GamePadInput.Instance.InteractAction, CharacterInputControl.Instance.OnInteractInput },
                { GamePadInput.Instance.MovementAction, CharacterInputControl.Instance.OnPlayerMoveInput },
                { GamePadInput.Instance.TriggerAction, OnTriggerInput },
                { GamePadInput.Instance.AimAction, OnAimInput },
                { GamePadInput.Instance.AimDirectionAction, OnAimDirectionInput },
                { GamePadInput.Instance.QuackAction, CharacterInputControl.Instance.OnQuackInput }
            };
            View.OnActiveViewChanged += OnActiveViewChanged;
            OnActiveViewChanged();
        }

        private void Update()
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
            else
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
            if (_hadBind)
            {
                return;
            }
            foreach (var pair in _bindingMap)
            {
                pair.Key.BindInput(pair.Value);
            }
            _hadBind = true;
        }

        private void OnDisable()
        {
            foreach (var pair in _bindingMap)
            {
                pair.Key.UnBindInput(pair.Value);
            }
        }

        private void OnDestroy()
        {
            View.OnActiveViewChanged -= OnActiveViewChanged;
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
            CharacterInputControl.Instance.OnPlayerTriggerInputUsingMouseKeyboard(context);
            if (context.started)
            {
                _triggerIsDown = true;
            }
            if (context.canceled)
            {
                _triggerIsDown = false;
            }
        }

        private void OnAimInput(InputAction.CallbackContext context)
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

    #region Reflection

        private FieldInfo GameCameraAimingTypeMethodInfo { get; } = typeof(GameCamera)
            .GetField("cameraAimingType", BindingFlags.NonPublic | BindingFlags.Instance);

        private GameCamera.CameraAimingTypes CameraAimingType =>
            (GameCamera.CameraAimingTypes)GameCameraAimingTypeMethodInfo.GetValue(GameCamera.Instance);

    #endregion
    }
}
