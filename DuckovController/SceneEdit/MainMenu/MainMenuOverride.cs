using Duckov.UI.Animations;
using DuckovController.SceneEdit.Other;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : AbstractPatch
    {
        private SelectionGroup<MainMenuBtnButtonOverride> _selectionGroup;

        private FadeGroup _fadeGroup;

        protected override void Awake()
        {
            _fadeGroup = GetComponent<FadeGroup>();
            _fadeGroup.OnShowComplete += OnFadeGroupCompleted;

            base.Awake();

            //选中第一
            EventSystem.current.SetSelectedGameObject(_menuButtonListLayout.GetChild(0).gameObject);
            //使用内置Index计数
            //不知道为什么，用 EventSystem + Navigate 无法正常运作，用土办法了
            _selectionGroup = new SelectionGroup<MainMenuBtnButtonOverride>(
                () =>
                {
                    var buttons = new MainMenuBtnButtonOverride[_menuButtonListLayout.childCount];
                    for (var i = 0; i < buttons.Length; i++)
                    {
                        buttons[i] = _menuButtonListLayout.GetChild(i).gameObject
                            .AddComponent<MainMenuBtnButtonOverride>();
                    }
                    return buttons;
                },
                (button, index) =>
                {
                    EventSystem.current.SetSelectedGameObject(_menuButtonListLayout.GetChild(index).gameObject);
                },
                loop: false
            );
        }

        private void OnFadeGroupCompleted(FadeGroup fadeGroup)
        {
            EventSystem.current.SetSelectedGameObject(_menuButtonListLayout.GetChild(0).gameObject);
        }

        private void OnConfirm(InputAction.CallbackContext obj)
        {
            var go = EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                if (go.TryGetComponent<MainMenuBtnButtonOverride>(out var handler))
                {
                    handler.Press();
                }
            }
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            onCancelBtnDown?.Invoke();
        }

        private void OnNavigateUp(InputAction.CallbackContext obj)
        {
            _selectionGroup.SelectPrev();
        }

        private void OnNavigateDown(InputAction.CallbackContext obj)
        {
            _selectionGroup.SelectNext();
        }
    }
}
