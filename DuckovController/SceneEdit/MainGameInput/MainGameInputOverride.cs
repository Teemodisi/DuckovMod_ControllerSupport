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
        private const float aim_smooth_time = 0.2f;

        private Vector2 _aimSmoothVel = Vector2.zero;

        private Dictionary<InputAction, Action<InputAction.CallbackContext>> _bindingMap;

        private bool _hadBind;

        private Vector2 _controllerDirection;

        private InputManager InputManager => CharacterInputControl.Instance.inputManager;

        private void Awake()
        {
            _bindingMap = new Dictionary<InputAction, Action<InputAction.CallbackContext>>
            {
                { GamePadInput.Instance.RunAction, CharacterInputControl.Instance.OnPlayerRunInput },
                { GamePadInput.Instance.RollAction, CharacterInputControl.Instance.OnDashInput },
                { GamePadInput.Instance.ReloadAction, CharacterInputControl.Instance.OnReloadInput },
                { GamePadInput.Instance.InteractAction, CharacterInputControl.Instance.OnInteractInput },
                { GamePadInput.Instance.MovementAction, CharacterInputControl.Instance.OnPlayerMoveInput },
                {
                    GamePadInput.Instance.TriggerAction,
                    CharacterInputControl.Instance.OnPlayerTriggerInputUsingMouseKeyboard
                },
                { GamePadInput.Instance.AimAction, OnAim },
                { GamePadInput.Instance.AimDirectionAction, OnAimDirection },
                { GamePadInput.Instance.QuackAction, CharacterInputControl.Instance.OnQuackInput }
            };
            View.OnActiveViewChanged += OnActiveViewChanged;
        }

        private void Update()
        {
            // View.ActiveView 
            // switch (CameraAimingType)
            // {
            //     case GameCamera.CameraAimingTypes.normal:
            //         Func();
            //         break;
            //     case GameCamera.CameraAimingTypes.bounds:
            //         Func();
            //         break;
            // }
            
            var w = Screen.width;
            var h = Screen.height;
            var center = new Vector2(w / 2f, h / 2f);
            //TODO：缺少偏移
            var controllerTarget = center + _controllerDirection.normalized * (Mathf.Min(w, h) * 0.4f);
            var cur = InputManager.MousePos;
            var next = Vector2.SmoothDamp(cur, controllerTarget, ref _aimSmoothVel, aim_smooth_time);
            var delta = next - cur;
            if (delta.magnitude > 0.01)
            {
                InputManager.SetAimInputUsingMouse(delta);
            }
            // var next = Vector2.MoveTowards(InputManager.MousePos , controllerTarget,aim_smooth_time * Time.deltaTime);
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

        private void OnActiveViewChanged()
        {
            Debug.Log($"ViewChanged {View.ActiveView?.name}");
        }

        private void OnAimDirection(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _controllerDirection = context.ReadValue<Vector2>();
            }
        }

        private void OnAim(InputAction.CallbackContext context)
        {
            if (context.started) { }
            if (context.canceled) { }
            // CharacterInputControl.Instance.inputManager.SetAimType(AimTypes.normalAim);
        }

    #region Reflection

        private FieldInfo GameCameraAimingTypeMethodInfo { get; } = typeof(GameCamera)
            .GetField("cameraAimingType", BindingFlags.NonPublic | BindingFlags.Instance);

        private GameCamera.CameraAimingTypes CameraAimingType =>
            (GameCamera.CameraAimingTypes)GameCameraAimingTypeMethodInfo.GetValue(GameCamera.Instance);

        private FieldInfo CharacterInputControlMousePosMethodInfo { get; } = typeof(CharacterInputControl)
            .GetField("mousePos", BindingFlags.NonPublic | BindingFlags.Instance);

        private Vector2 PlayerMousePos
        {
            get => (Vector2)CharacterInputControlMousePosMethodInfo.GetValue(CharacterInputControl.Instance);
            set => CharacterInputControlMousePosMethodInfo.SetValue(CharacterInputControl.Instance, value);
        }

    #endregion
    }
}
