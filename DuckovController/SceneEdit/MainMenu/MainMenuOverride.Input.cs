using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        //不知道为什么，用 EventSystem + Navigate 无法正常运作，用土办法了
        private int _selectedIndex;

        public event Action onCancelBtnDown;

        private void RegInput()
        {
            GamePadInput.Instance.ConfirmAction.performed += OnConfirm;
            GamePadInput.Instance.CancelAction.performed += OnCancel;
            GamePadInput.Instance.NavigateUpAction.performed += OnNavigateUp;
            GamePadInput.Instance.NavigateDownAction.performed += OnNavigateDown;

            //好像碳酸本来就没绑这些交互在主界面
            // GameManager.MainPlayerInput.actions["UI_Cancel"].AddBinding("<Gamepad>/buttonEast");
        }

        private void UnRegInput()
        {
            GamePadInput.Instance.ConfirmAction.performed -= OnConfirm;
            GamePadInput.Instance.CancelAction.performed -= OnCancel;
            GamePadInput.Instance.NavigateUpAction.performed -= OnNavigateUp;
            GamePadInput.Instance.NavigateDownAction.performed -= OnNavigateDown;
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
