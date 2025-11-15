using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class PlayerStatusViewOverride : MonoBehaviour
    {
        private ScrollRect _scrollRect;

        private ScrollViewGamepadControl _scrollViewGamepadControl;

        private void Awake()
        {
            _scrollRect = GetComponentInChildren<ScrollRect>();
            _scrollViewGamepadControl = _scrollRect.gameObject.AddComponent<ScrollViewGamepadControl>();
            Patch();
            InitInput();
        }

        private void OnEnable()
        {
            _inputActionMap?.Enable();
        }

        private void OnDisable()
        {
            _inputActionMap?.Disable();
        }

        private void OnMoveListInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _scrollViewGamepadControl.Move(context.ReadValue<Vector2>());
            }
            if (context.canceled)
            {
                _scrollViewGamepadControl.Move(Vector2.zero);
            }
        }
    }
}
