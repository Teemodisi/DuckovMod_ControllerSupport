using DuckovController.Helper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.Other
{
    public class SceneLoaderOverride : MonoBehaviour
    {
        private OnPointerClick _onPointerClick;

        private InputAction _action;

        private void Awake()
        {
            _onPointerClick = GetComponent<OnPointerClick>();
            _action = new InputAction("ConfirmBtn", InputActionType.Button);
            _action.AddBinding(InputSystemUtils.BindingBButton);
            _action.performed += OnConfirmBtnDown;
        }

        //这里会根据SceneLoader被开启和关闭
        private void OnEnable()
        {
            _action.Enable();
        }

        private void OnDisable()
        {
            _action.Disable();
        }

        private void OnConfirmBtnDown(InputAction.CallbackContext obj)
        {
            _onPointerClick.gameObject.EmitEventPointerClickAndDownBtnLeft();
        }
    }
}
