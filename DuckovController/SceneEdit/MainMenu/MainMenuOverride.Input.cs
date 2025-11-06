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

        //不知道为什么，用 EventSystem + Navigate 无法正常运作，用土办法了
        private int _selectedIndex;

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
            var go = EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                go.GetComponent<ISubmitHandler>()?.OnSubmit(new BaseEventData(EventSystem.current));
            }
        }

        private void OnNavigateUp(InputAction.CallbackContext obj)
        {
            --_selectedIndex;
            if (_selectedIndex < 0)
            {
                _selectedIndex = _buttons.Length - 1;
            }
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(_selectedIndex).gameObject);
        }

        private void OnNavigateDown(InputAction.CallbackContext obj)
        {
            ++_selectedIndex;
            if (_selectedIndex >= _buttons.Length)
            {
                _selectedIndex = 0;
            }
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(_selectedIndex).gameObject);
        }
    }
}
