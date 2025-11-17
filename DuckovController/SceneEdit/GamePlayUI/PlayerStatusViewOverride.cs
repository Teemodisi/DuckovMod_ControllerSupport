using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class PlayerStatusViewOverride : AbstractPatch
    {
        private ScrollRect _scrollRect;

        private ScrollViewGamepadControl _scrollViewGamepadControl;

        protected override void Awake()
        {
            _scrollRect = GetComponentInChildren<ScrollRect>();
            _scrollViewGamepadControl = _scrollRect.gameObject.AddComponent<ScrollViewGamepadControl>();
            base.Awake();
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
