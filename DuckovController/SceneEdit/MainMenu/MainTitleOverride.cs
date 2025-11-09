using DuckovController.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public class MainTitleOverride : MonoBehaviour
    {
        private static PointerEventData s_PointerEventData;

        private Title _title;

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
            _title = gameObject.GetComponent<Title>();
            Patch();
        }

        private void OnEnable()
        {
            GamePadInput.Instance.ConfirmAction.performed += OnClick;
        }

        private void OnDisable()
        {
            GamePadInput.Instance.ConfirmAction.performed -= OnClick;
            s_PointerEventData =  null;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
#if DEBUG
            Debug.Log($"{Utils.ModName} MainTitleOverride: OnClick");
#endif
            _title.OnPointerClick(PointerEventData);
        }

        private void Patch()
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
    }
}
