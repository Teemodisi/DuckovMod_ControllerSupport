using DuckovController.Helper;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.GamePlayUI
{
    public partial class LootViewOverride
    {
        private bool _leftTriggerPressed;

        private Vector2 _selectDirection;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            var leftTrigger = inputActionMap.AddAction("LT", InputActionType.Button);
            leftTrigger.AddBinding(InputSystemUtils.BindingLeftTrigger);
            var leftAxis = inputActionMap.AddAction("LeftAxis", expectedControlLayout: "Vector2");
            leftAxis.AddBinding(InputSystemUtils.BindingLeftStick);

            leftTrigger.BindInput(OnLeftTrigger);
            leftAxis.BindInput(OnLeftAxis);
        }

        private void OnLeftAxis(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            if (value.sqrMagnitude > 0.01f)
            {
                _selectDirection = value.normalized;
            }
            else
            {
                _selectDirection = Vector2.zero;
            }
        }

        private void OnLeftTrigger(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _leftTriggerPressed = true;
            }
            else if (context.canceled)
            {
                _leftTriggerPressed = false;
            }
        }

    }
}
