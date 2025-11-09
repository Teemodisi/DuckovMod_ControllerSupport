using Duckov.UI.Animations;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DuckovController.SceneEdit.MainMenu
{
    public partial class MainMenuOverride
    {
        [CanBeNull]
        private TMP_FontAsset _fontTemplate;

        public RectTransform MenuButtonListLayout { get; private set; }

        public RectTransform MenuPadTipsLayout { get; private set; }

        public void Patch()
        {
            //目前位置 Canvas/MainMenuContainer/Menu/MainGroup
            var obj = transform.Find("Layout");
            if (obj == null)
            {
                Debug.LogError("找不到主菜单的Layout");
                return;
            }
            MenuButtonListLayout = obj!.GetComponent<RectTransform>();
            if (MenuButtonListLayout == null)
            {
                Debug.LogError("找不到主菜单的Layout的RectTransform");
                return;
            }
            var tmp = MenuButtonListLayout.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp == null)
            {
                Debug.LogError("找不到模板字体");
                return;
            }
            _fontTemplate = tmp.font;
            UIStyle.currentFont = _fontTemplate;

            MenuPadTipsLayout = new GameObject("ControllerTips").AddComponent<RectTransform>();
            MenuPadTipsLayout.SetParent(MenuButtonListLayout.parent, false);
            MenuPadTipsLayout.anchorMin = new Vector2(1, 0);
            MenuPadTipsLayout.anchorMax = new Vector2(1, 0);
            MenuPadTipsLayout.pivot = new Vector2(1, 0);
            MenuPadTipsLayout.sizeDelta = new Vector2(500, 40);
            var horGroup = MenuPadTipsLayout.gameObject.AddComponent<HorizontalLayoutGroup>();
            horGroup.spacing = 10;
            horGroup.childAlignment = TextAnchor.MiddleRight;
            horGroup.childControlWidth = false;
            horGroup.childControlHeight = false;
            horGroup.childForceExpandWidth = false;
            horGroup.childForceExpandHeight = true;
            horGroup.reverseArrangement = true;
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, UIStyle.GamePadButton.A, L10N.Instance.Confirm);
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, UIStyle.GamePadButton.Up, L10N.Instance.NavigateUp);
            UIStyle.DrawPadButtonTips(MenuPadTipsLayout, UIStyle.GamePadButton.Down, L10N.Instance.NavigateDown);
            UpdateTipsRectPos();

            //更改UI Hovering样式 改为描边嗷
            var btnAnims = MenuButtonListLayout.gameObject.GetComponentsInChildren<ButtonAnimation>();
            foreach (var buttonAnimation in btnAnims)
            {
                buttonAnimation.gameObject.AddComponent<MainMenuBtnStyleOverride>();
            }
        }

        private void OnFadeGroupCompleted(FadeGroup fadeGroup)
        {
            EventSystem.current.SetSelectedGameObject(MenuButtonListLayout.GetChild(0).gameObject);
        }

        private void UpdateTipsRectPos()
        {
            //TODO:其实应该将这个提示层级提升
            //用左边最后一个按钮做参考进行对齐
            //不知道为什么未显示前和动画后是两个偏移位置，大概是动画造成，通过监听更新这个时机对齐
            // var lastBtn = MenuButtonListLayout.GetChild(MenuButtonListLayout.childCount - 1)
            //     .GetComponent<RectTransform>();
            // lastBtn.LogRectTransformInfo();
            // MenuButtonListLayout.LogRectTransformInfo();
            // var offset = MenuButtonListLayout.sizeDelta.y - (-lastBtn.anchoredPosition.y + lastBtn.sizeDelta.y * 0.5f);
            //已知是16
            MenuPadTipsLayout.anchoredPosition = new Vector2(-100, 50);
        }
    }
}
