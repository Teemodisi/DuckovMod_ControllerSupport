using System;
using DuckovController.Helper;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        private InputAction _navigateUpAction;

        private InputAction _navigateDownAction;

        private InputAction _confirmAction;

        private InputAction _cancelAction;

        public event Action onCancelBtnDown;

        protected override void InitInput(InputActionMap inputActionMap)
        {
            _confirmAction = inputActionMap.AddAction("Confirm", InputActionType.Button);
            _confirmAction.AddBinding(InputSystemUtils.BindingBButton);

            _cancelAction = inputActionMap.AddAction("Cancel", InputActionType.Button);
            _cancelAction.AddBinding(InputSystemUtils.BindingAButton);

            _navigateUpAction = inputActionMap.AddAction("NavigationUp", InputActionType.Button);
            _navigateUpAction.AddBinding(InputSystemUtils.BindingDpadUp);

            _navigateDownAction = inputActionMap.AddAction("NavigationDown", InputActionType.Button);
            _navigateDownAction.AddBinding(InputSystemUtils.BindingDpadDown);

            _confirmAction.performed += OnConfirm;
            _cancelAction.performed += OnCancel;
            _navigateUpAction.performed += OnNavigateUp;
            _navigateDownAction.performed += OnNavigateDown;
        }
    }
}
