using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        private InputActionMap _inputActionMap;

        private InputAction _navigateUpAction;

        private InputAction _navigateDownAction;

        private InputAction _confirmAction;

        private void InitInputMap()
        {
            _inputActionMap = new InputActionMap("GamepadControls");
            _confirmAction = _inputActionMap.AddAction("Confirm", InputActionType.Button);
            _confirmAction.AddBinding("<Gamepad>/buttonSouth");

            _navigateUpAction = _inputActionMap.AddAction("NavigationUp", InputActionType.Button);
            _navigateUpAction.AddBinding("<Gamepad>/dpad/up");

            _navigateDownAction = _inputActionMap.AddAction("NavigationDown", InputActionType.Button);
            _navigateDownAction.AddBinding("<Gamepad>/dpad/down");

            _confirmAction.performed += OnConfirm;
            _navigateUpAction.performed += OnNavigateUp;
            _navigateDownAction.performed += OnNavigateDown;
        }

        private void OnConfirm(InputAction.CallbackContext obj)
        {
            if (EventSystem.current.alreadySelecting)
            {
                var com = EventSystem.current.currentSelectedGameObject.GetComponent<ISubmitHandler>();
                if (com != null)
                {
                    com.OnSubmit(null);
                }
            }
            Debug.Log($"Confirm {EventSystem.current.alreadySelecting}");
        }

        private void OnNavigateUp(InputAction.CallbackContext obj)
        {
            if (EventSystem.current.alreadySelecting)
            {
                var com = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
                if (com != null)
                {
                    EventSystem.current.SetSelectedGameObject(com.navigation.selectOnUp.gameObject);
                }
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(0).gameObject);
            }
            Debug.Log($"OnNavigateUp {EventSystem.current.alreadySelecting}");
        }

        private void OnNavigateDown(InputAction.CallbackContext obj)
        {
            if (EventSystem.current.alreadySelecting)
            {
                var com = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
                if (com != null)
                {
                    EventSystem.current.SetSelectedGameObject(com.navigation.selectOnDown.gameObject);
                }
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(0).gameObject);
            }
            Debug.Log($"OnNavigateDown {EventSystem.current.alreadySelecting}");
        }
    }
}
