using System;
using Duckov.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        private InputActionMap _inputActionMap;

        private InputAction _navigateUpAction;

        private InputAction _navigateDownAction;

        private InputAction _confirmAction;

        private InputAction _cancelAction;

        public event Action onCancelBtnDown;
        
        //不知道为什么，用 EventSystem + Navigate 无法正常运作，用土办法了
        private int _selectedIndex;

        private void InitInputMap()
        {
            _inputActionMap = new InputActionMap("GamepadControls");
            _confirmAction = _inputActionMap.AddAction("Confirm", InputActionType.Button);
            _confirmAction.AddBinding("<Gamepad>/buttonSouth");

            _cancelAction = _inputActionMap.AddAction("Cancel", InputActionType.Button);
            _cancelAction.AddBinding("<Gamepad>/buttonEast");

            _navigateUpAction = _inputActionMap.AddAction("NavigationUp", InputActionType.Button);
            _navigateUpAction.AddBinding("<Gamepad>/dpad/up");

            _navigateDownAction = _inputActionMap.AddAction("NavigationDown", InputActionType.Button);
            _navigateDownAction.AddBinding("<Gamepad>/dpad/down");

            _confirmAction.performed += OnConfirm;
            _cancelAction.performed += OnCancel;
            _navigateUpAction.performed += OnNavigateUp;
            _navigateDownAction.performed += OnNavigateDown;

            // GameManager.MainPlayerInput.actions["UI_Cancel"].AddBinding("<Gamepad>/buttonEast");
        }

        private void OnConfirm(InputAction.CallbackContext obj)
        {
            var go = EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                if (go.TryGetComponent<MainMenuBtnButtonOverride>(out var handler))
                {
                    handler.Press();
                }
            }
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            onCancelBtnDown?.Invoke();
        }

        private void OnNavigateUp(InputAction.CallbackContext obj)
        {
            _selectedIndex = Mathf.Max(_selectedIndex - 1, 0);
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(_selectedIndex).gameObject);
        }

        private void OnNavigateDown(InputAction.CallbackContext obj)
        {
            _selectedIndex = Mathf.Min(_selectedIndex + 1, _buttons.Length - 1);
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(_selectedIndex).gameObject);
        }
    }
}
