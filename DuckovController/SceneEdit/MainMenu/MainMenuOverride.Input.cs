using System;
using DuckovController.Helper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
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
#if DEBUG
            Debug.Log($"{Utils.ModName} MainMenu OnConfirm");
#endif
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
#if DEBUG
            Debug.Log($"{Utils.ModName} MainMenu OnNavigateUp");
#endif
            _selectionGroup.SelectPrev();
        }

        private void OnNavigateDown(InputAction.CallbackContext obj)
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} MainMenu OnNavigateDown");
#endif
            _selectionGroup.SelectNext();
        }
    }
}
