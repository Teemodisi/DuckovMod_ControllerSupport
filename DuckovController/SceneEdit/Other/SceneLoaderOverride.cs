using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.Other
{
    public class SceneLoaderOverride : MonoBehaviour
    {
        private static PointerEventData s_PointerEventData;

        private OnPointerClick _onPointerClick;

        private InputAction _action;

        private static PointerEventData PointerEventData
        {
            get
            {
                if (s_PointerEventData == null)
                {
                    s_PointerEventData = new PointerEventData(EventSystem.current)
                    {
                        button = PointerEventData.InputButton.Left
                    };
                }
                return s_PointerEventData;
            }
        }

        private void Awake()
        {
            _onPointerClick = GetComponent<OnPointerClick>();
            _action = new InputAction("ConfirmBtn", InputActionType.Button);
            _action.AddBinding("<Gamepad>/buttonSouth");
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
            //诶 这里刚好接口没写公共但是 event 没写规范就直接用了嘻嘻
            _onPointerClick.onPointerClick.Invoke(PointerEventData);
        }
    }
}
