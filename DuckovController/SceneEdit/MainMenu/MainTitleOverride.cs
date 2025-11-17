using DuckovController.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public class MainTitleOverride : AbstractPatch
    {
        private Title _title;

        protected override void Awake()
        {
            _title = gameObject.GetComponent<Title>();
            base.Awake();
        }

        protected override void InitInput(InputActionMap inputActionMap)
        {
            var confirmAction = inputActionMap.AddAction("Confirm", InputActionType.Button);
            confirmAction.AddBinding("<Gamepad>/buttonSouth");
            confirmAction.performed += OnClick;
        }

        protected override void Patch()
        {
            if (!Utils.FindGameObject("TimelineContent", out Transform timelineContent))
            {
                Debug.Log("找不到 TimelineContent");
                return;
            }
            var anyKeyText = timelineContent.Find("LOGO/PressAnyKeyContinue").GetComponent<TextMeshPro>();
            if (anyKeyText == null)
            {
                Debug.Log("找不到 anyKeyText");
                return;
            }
            anyKeyText.text = L10N.Instance.TitlePressAnyKey;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} {GetType().Name} OnClick");
#endif
            _title.OnPointerClick(new PointerEventData(EventSystem.current)
            {
                button = PointerEventData.InputButton.Left
            });
        }
    }
}
