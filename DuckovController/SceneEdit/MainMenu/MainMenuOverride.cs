using Duckov.UI.Animations;
using DuckovController.SceneEdit.Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride : MonoBehaviour
    {

        private SelectionGroup<MainMenuBtnButtonOverride> _selectionGroup;
        
        private FadeGroup _fadeGroup;

        private void Awake()
        {
            _fadeGroup = GetComponent<FadeGroup>();
            _fadeGroup.OnShowComplete += OnFadeGroupCompleted;
            Patch();
            var buttons  = new MainMenuBtnButtonOverride[MenuButtonListLayout.childCount];
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i] = MenuButtonListLayout.GetChild(i).gameObject.AddComponent<MainMenuBtnButtonOverride>();
            }
            //选中第一
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(0).gameObject);
            //使用内置Index计数
            //不知道为什么，用 EventSystem + Navigate 无法正常运作，用土办法了
            _selectionGroup = new SelectionGroup<MainMenuBtnButtonOverride>(
                buttons,
                (button, index) =>
                {
                    EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(index).gameObject);
                },
                loop: false
            );
        }


        private void OnEnable()
        {
            RegInput();
        }

        private void OnDisable()
        {
            UnRegInput();
        }
    }
}
